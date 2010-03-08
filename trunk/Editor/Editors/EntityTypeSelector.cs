//
// EntityTypeSeletor.cs
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Galaxy
{
    /// <summary>
    /// Entity type selector control. Allows selection from preset entity list.
    /// </summary>
    [System.ComponentModel.DesignerCategory("Code")]
    public class CTypeSelectorControl
        : ListBox
    {
        public List<Type> SpawnableEntityTypes;

        public Type Result = typeof(CEntity);

        public CTypeSelectorControl(List<Type> types)
        {
            SpawnableEntityTypes = types;
            InitializeComponent();
            this.SpawnableEntityTypes = types;
            this.BorderStyle = BorderStyle.None;
            Result = typeof(CEntity);
        }

        private void InitializeComponent()
        {
            foreach (Type type in SpawnableEntityTypes)
            {
                string typename = type.ToString().Substring("Galaxy.".Length);
                this.Items.Add(typename);
            }
        }
    }

    public class CEntityTypes
    {
        public static List<Type> SpawnableEntityTypes = new List<Type>()
        {
            typeof(CBonus),
            typeof(CPowerup),
        };
    }

    public class CEnemyTypes
    {
        public static List<Type> SpawnableEntityTypes = new List<Type>()
        {
            typeof(CBall),
            typeof(CBeamer),
            typeof(CIsosceles),
            typeof(CShootBall),
            typeof(CSpike),
            typeof(CTurret),
        };
    }

    /// <summary>
    /// Type editor which allows using custom type listbox on entity Type properties.
    /// </summary>
    public class CTypeSelector<Types>
        : UITypeEditor
    {
        private CTypeSelectorControl TypeSelector;
        private IWindowsFormsEditorService EditorService;

        public CTypeSelector()
        {
            FieldInfo field = typeof(Types).GetField("SpawnableEntityTypes", BindingFlags.Static | BindingFlags.Public);
            List<Type> types = (List<Type>)field.GetValue(null);
            TypeSelector = new CTypeSelectorControl(types);
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
            CTypeSelectorControl selector = sender as CTypeSelectorControl;
            selector.Result = Type.GetType("Galaxy." + selector.SelectedItem);
            EditorService.CloseDropDown();
        }
    }
}
