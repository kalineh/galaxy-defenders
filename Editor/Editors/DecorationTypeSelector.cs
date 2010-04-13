//
// DecorationTypeSeletor.cs
//

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Xna.Framework.Storage;

namespace Galaxy
{
    /// <summary>
    /// Entity type selector control. Allows selection from preset entity list.
    /// </summary>
    [System.ComponentModel.DesignerCategory("Code")]
    public class CDecorationTypeSelectorControl
        : ListBox
    {
        public List<string> Types;
        public string Result = "";

        public CDecorationTypeSelectorControl(List<string> types)
        {
            Types = types;
            InitializeComponent();
            this.Types = types;
            this.BorderStyle = BorderStyle.None;
        }

        private void InitializeComponent()
        {
            foreach (string type in Types)
            {
                this.Items.Add(type);
            }
        }
    }

    /// <summary>
    /// Type editor which allows using custom type listbox on entity Type properties.
    /// </summary>
    public class CDecorationSelector
        : UITypeEditor
    {
        private CDecorationTypeSelectorControl TypeSelector;
        private IWindowsFormsEditorService EditorService;

        public CDecorationSelector()
        {
            // TODO: generic place
            List<string> types = new List<string>();
            string fullpath = Directory.GetCurrentDirectory();
            string texture_directory = fullpath + "\\..\\..\\..\\Content\\Textures\\Decoration";
            string[] filepaths = Directory.GetFiles(texture_directory, "*.png");
            foreach (string filepath in filepaths)
            {
                types.Add(Path.GetFileNameWithoutExtension(filepath));
            }

            TypeSelector = new CDecorationTypeSelectorControl(types);
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
            CDecorationTypeSelectorControl selector = sender as CDecorationTypeSelectorControl;
            selector.Result = (string)selector.SelectedItem;
            EditorService.CloseDropDown();
        }
    }
}
