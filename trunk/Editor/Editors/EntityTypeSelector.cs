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
    namespace Editor
    {
        /// <summary>
        /// Entity type selector control. Allows selection from preset entity list.
        /// </summary>
        [System.ComponentModel.DesignerCategory("Code")]
        public class CEntityTypeSelectorControl 
            : ListBox
        {
            // TODO: should we put this in the spawner entity?
            public static List<Type> SpawnableEntityTypes = new List<Type>()
            {
                typeof(CAsteroid),
                typeof(CBeard),
                typeof(CPewPew),
                typeof(CLaser),
                typeof(CSinBall),
                typeof(CTurret),
                typeof(CBonus),
                typeof(CBuilding),
                typeof(CSpacePlatform),
                typeof(CBigSpacePlatform),
            };

            public Type Result = typeof(CEntity);

            public CEntityTypeSelectorControl()
            {
                InitializeComponent();
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
