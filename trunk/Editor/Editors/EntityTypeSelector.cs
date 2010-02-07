//
// EntityTypeSeletor.cs
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
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    namespace Editor
    {
        /// <summary>
        /// Entity type selector control. Allows selection from preset entity list.
        /// </summary>
        [System.ComponentModel.DesignerCategory("Code")]
        public class CEntityTypeSelectorControl 
            : ListBox
        {
            public Type Result = typeof(CEntity);

            public CEntityTypeSelectorControl()
            {
                InitializeComponent();
                this.BorderStyle = BorderStyle.None;
                Result = typeof(CEntity);
            }

            private void InitializeComponent()
            {
                foreach (Type type in CEditorEntityTypes.EntityTypeList)
                {
                    string typename = type.ToString().Substring("Galaxy.".Length);
                    this.Items.Add(typename);
                }
            }

            /// <summary>
            /// Generate from all assembly types.
            /// Not currently used.
            /// </summary>
            private void AutoGenerateList()
            {
                Assembly assembly = Assembly.GetAssembly(typeof(Galaxy.CEntity));
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(CEntity)))
                    {
                        string typename = type.ToString().Substring("Galaxy.".Length);
                        this.Items.Add(typename);
                    }
                }
            }
        }

        /// <summary>
        /// Type editor which allows using custom type listbox on entity Type properties.
        /// </summary>
        public class CEntityTypeSelector
            : UITypeEditor
        {
            private CEntityTypeSelectorControl TypeSelector;
            private IWindowsFormsEditorService EditorService;

            public CEntityTypeSelector()
            {
                TypeSelector = new CEntityTypeSelectorControl();
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
                TypeSelector.SelectedIndexChanged += new EventHandler(TypeSelector_SelectedIndexChanged);

                editor_service.DropDownControl(TypeSelector);

                TypeSelector.SelectedIndexChanged -= TypeSelector_SelectedIndexChanged;
                EditorService = null;

                return TypeSelector.Result;
            }

            void TypeSelector_SelectedIndexChanged(object sender, EventArgs e)
            {
                CEntityTypeSelectorControl selector = sender as CEntityTypeSelectorControl;
                selector.Result = Type.GetType("Galaxy." + selector.SelectedItem);
                EditorService.CloseDropDown();
            }
        }
    }
}
