//
// EntityMoverPresetSelector.cs
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Globalization;

namespace Galaxy
{
    /// <summary>
    /// Entity type selector control. Allows selection from preset entity list.
    /// </summary>
    [System.ComponentModel.DesignerCategory("Code")]
    public class CEntityMoverPresetSelectorControl 
        : ListBox
    {
        public static Dictionary<string, string> MoverPresets;

        static CEntityMoverPresetSelectorControl()
        {
            MoverPresets = new Dictionary<string, string>();

            MethodInfo[] methods = typeof(CMoverPresets).GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (MethodInfo method in methods)
            {
                // NOTE: ignore this helper function
                if (method.Name == "FromName")
                    continue;

                string name = method.ToString();
                char[] seperators = { ' ', '(' };
                string stripped = name.Split(seperators)[1];
                MoverPresets[stripped] = stripped;
            }
        }

        public string Result = null;

        public CEntityMoverPresetSelectorControl()
        {
            InitializeComponent();
            this.BorderStyle = BorderStyle.None;
            Result = "IgnoreCamera";
        }

        private void InitializeComponent()
        {
            foreach (KeyValuePair<string, string> items in MoverPresets)
            {
                string text = items.Key;
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
            string selected = (string)selector.SelectedItem;
            selector.Result = selected;
            EditorService.CloseDropDown();
        }
    }
}
