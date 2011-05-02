//
// TextureSelector.cs
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
using System.Drawing;

namespace Galaxy
{
    /// <summary>
    /// TextureSelector control.
    /// </summary>
    [System.ComponentModel.DesignerCategory("Code")]
    public class CTextureSelectorListControl
        : ListBox
    {
        public List<string> Types;
        public string Result = "";

        public CTextureSelectorListControl(string relative_directory)
        {
            List<string> types = new List<string>();
            string fullpath = Directory.GetCurrentDirectory();
            string texture_directory = fullpath + relative_directory;
            string[] filepaths = Directory.GetFiles(texture_directory, "*.png");
            foreach (string filepath in filepaths)
            {
                types.Add(Path.GetFileNameWithoutExtension(filepath));
            }

            this.Types = types;
            this.BorderStyle = BorderStyle.None;
            InitializeComponent();
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
    public class CTextureSelectorList<CategoryProxy>
        : UITypeEditor
    {
        private CTextureSelectorControl SelectorControl;
        private IWindowsFormsEditorService EditorService;

        public CTextureSelectorList()
        {
            Type type = typeof(CategoryProxy);
            string texture_category = (string)type.GetField("Value").GetValue(null);
            SelectorControl = new CTextureSelectorControl("\\..\\..\\..\\Content\\Textures\\" + texture_category);
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
            SelectorControl.SelectedIndexChanged += new EventHandler(SelectorControl_SelectedIndexChanged);

            editor_service.DropDownControl(SelectorControl);

            SelectorControl.SelectedIndexChanged -= SelectorControl_SelectedIndexChanged;
            EditorService = null;

            return SelectorControl.Result;
        }

        void SelectorControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CTextureSelectorControl selector = sender as CTextureSelectorControl;
            selector.Result = (string)selector.SelectedItem;
            EditorService.CloseDropDown();
        }
    }

    /// <summary>
    /// TextureSelector control.
    /// </summary>
    [System.ComponentModel.DesignerCategory("Code")]
    public class CTextureSelectorControl
        : ListBox
    {
        public List<string> Types;
        public List<Image> Images;
        public string Result = "";

        public CTextureSelectorControl(string relative_directory)
        {
            List<string> types = new List<string>();
            string fullpath = Directory.GetCurrentDirectory();
            string texture_directory = fullpath + relative_directory;
            string[] filepaths = Directory.GetFiles(texture_directory, "*.png");

            this.Types = new List<string>(filepaths);
            this.Images = new List<Image>();
            this.BorderStyle = BorderStyle.None;
            this.Size = new Size(128, 600);

            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.DrawItem += DrawItemOverride;
            this.MeasureItem += MeasureItemOverride;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            foreach (string type in Types)
            {
                Image image = Image.FromFile(type);
                this.Items.Add(type);
                this.Images.Add(image);
            }
        }

        private void DrawItemOverride(object sender, DrawItemEventArgs args)
        {
            Font font = new Font(FontFamily.GenericMonospace, 8);
            int index = args.Index;
            Image image = Images[index];
            string type = Path.GetFileNameWithoutExtension(Types[index]);

            args.Graphics.DrawRectangle(Pens.Black, args.Bounds.X, args.Bounds.Y, 32, 32);
            args.Graphics.DrawImage(image, args.Bounds.X, args.Bounds.Y, 32, 32);
            args.Graphics.DrawString(type, font, Brushes.Black, new PointF(args.Bounds.X + 32.0f, args.Bounds.Y));
            args.DrawFocusRectangle();
        }

        private void MeasureItemOverride(object sender, MeasureItemEventArgs args)
        {
            args.ItemWidth = 32;
            args.ItemHeight += 20;
        }
    }

    public class CTextureSelector<CategoryProxy>
        : UITypeEditor
    {
        private CTextureSelectorControl SelectorControl;
        private IWindowsFormsEditorService EditorService;

        public CTextureSelector()
        {
            Type type = typeof(CategoryProxy);
            string texture_category = (string)type.GetField("Value").GetValue(null);
            SelectorControl = new CTextureSelectorControl("\\..\\..\\..\\Content\\Textures\\" + texture_category);
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
            SelectorControl.SelectedIndexChanged += new EventHandler(SelectorControl_SelectedIndexChanged);

            editor_service.DropDownControl(SelectorControl);

            SelectorControl.SelectedIndexChanged -= SelectorControl_SelectedIndexChanged;
            EditorService = null;

            return SelectorControl.Result;
        }

        void SelectorControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CTextureSelectorControl selector = sender as CTextureSelectorControl;
            selector.Result = Path.GetFileNameWithoutExtension((string)selector.SelectedItem);
            EditorService.CloseDropDown();
        }
    }

    public class CTextureSelectoryCategoryDecoration { public static string Value = "Decoration"; }
    public class CTextureSelectoryCategoryStatic { public static string Value = "Static"; }
}
