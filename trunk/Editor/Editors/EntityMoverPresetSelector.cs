//
// EntityMoverPresetSelector.cs
//

using System;
using System.Drawing.Design;
using System.Globalization;
using System.ComponentModel;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    namespace Editor
    {
        // TODO: put this elsewhere?
        public delegate CMover MoverPresetFunction(float speed);

        /// <summary>
        /// Entity type selector control. Allows selection from preset entity list.
        /// </summary>
        [System.ComponentModel.DesignerCategory("Code")]
        public class CEntityMoverPresetSelectorControl 
            : ListBox
        {
            public static Dictionary<string, MoverPresetFunction> MoverPresets;

            static CEntityMoverPresetSelectorControl()
            {
                MoverPresets = new Dictionary<string, MoverPresetFunction>();

                MethodInfo[] methods = typeof(CMoverPresets).GetMethods(BindingFlags.Public | BindingFlags.Static);
                foreach (MethodInfo method in methods)
                {
                    Delegate delegate_ = Delegate.CreateDelegate(typeof(MoverPresetFunction), method);
                    MoverPresetFunction function = (MoverPresetFunction)delegate_;
                    string name = method.ToString();
                    char[] seperators = { ' ', '(' };
                    string stripped = name.Split(seperators)[1];
                    MoverPresets[stripped] = function;
                }
            }

            public CMover Result = null;

            public CEntityMoverPresetSelectorControl()
            {
                InitializeComponent();
                this.BorderStyle = BorderStyle.None;
                Result = CMoverPresets.MoveDown(1.0f);
            }

            private void InitializeComponent()
            {
                foreach (KeyValuePair<string, MoverPresetFunction> items in MoverPresets)
                {
                    string text = items.Key;
                    //string typename = type.ToString().Substring("Galaxy.".Length);
                    this.Items.Add(items.Key);
                }
            }
        }


        /// <summary>
        /// Select from preset mover list.
        /// </summary>
        public class CEntityMoverPresetSelector
            : UITypeEditor
        {
            private CEntityMoverPresetSelectorControl MoverPresetSelector;
            private IWindowsFormsEditorService EditorService;

            public CEntityMoverPresetSelector()
            {
                MoverPresetSelector = new CEntityMoverPresetSelectorControl();
            }

            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.DropDown;
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                if (provider == null)
                    return base.EditValue(context, provider, value);

                IWindowsFormsEditorService editor_service = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
                if (editor_service == null)
                    return base.EditValue(context, provider, value);

                EditorService = editor_service;
                MoverPresetSelector.SelectedIndexChanged += new EventHandler(MoverPresetSelector_SelectedIndexChanged);

                editor_service.DropDownControl(MoverPresetSelector);

                MoverPresetSelector.SelectedIndexChanged -= MoverPresetSelector_SelectedIndexChanged;
                EditorService = null;

                return MoverPresetSelector.Result;
            }

            void MoverPresetSelector_SelectedIndexChanged(object sender, EventArgs e)
            {
                CEntityMoverPresetSelectorControl selector = sender as CEntityMoverPresetSelectorControl;
                string method_name = (string)selector.SelectedItem;
                MethodInfo method = typeof(CMoverPresets).GetMethod(method_name);
                MoverPresetFunction function = (MoverPresetFunction)Delegate.CreateDelegate(typeof(MoverPresetFunction), method);
                CMover result = function(1.0f);
                selector.Result = result;
                EditorService.CloseDropDown();
            }
        }
    }
}
