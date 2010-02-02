//
// EditorElements.cs
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
        using WinPoint = System.Drawing.Point;
        using XnaPoint = Microsoft.Xna.Framework.Point;
        using XnaColor = Microsoft.Xna.Framework.Graphics.Color;

        public class CUnknown
            : CEntity
        {
            //[TypeConverter(typeof(ExpandableObjectConverter))]
            public CStageElement StageElement { get; set; }

            public CUnknown(CWorld world, CStageElement stage_element)
                : base(world, "EditorEntity")
            {
                Physics = new CPhysics();
                StageElement = stage_element;
            }

            public override float GetRadius()
            {
                return 15.0f;
            }
        }

        public class CEntityTypeSelectorControl 
            : ListBox
        {
            public Type Result = null;

            public CEntityTypeSelectorControl()
            {
                InitializeComponent();
                this.BorderStyle = BorderStyle.None;
                Result = typeof(CEntity);
            }

            private void InitializeComponent()
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

        public class CEntityTypeConverter
            : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(String))
                    return true;

                return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                return base.ConvertFrom(context, culture, value);
            }

            public new PropertyDescriptorCollection GetProperties(object value)
            {
                return new PropertyDescriptorCollection(new PropertyDescriptor[] { });
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return base.CanConvertTo(context, destinationType);
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public new ICollection GetStandardValues()
            {
                return new List<string>() { "one", "two", "three" };
            }
        }

        public class CSpawnerEntity
            : CEntity
        {
            [TypeConverter(typeof(CSpawnerEntityConverter))]
            public CStageElement StageElement { get; set; }

            public CSpawnerEntity(CWorld world, Galaxy.CSpawnerEntity element)
                : base(world, "EditorEntitySpawnEntity")
            {
                StageElement = element;
                Physics = new CPhysics();
                Visual = new CVisual(CContent.LoadTexture2D(world.Game, "Textures/Enemy/SinBall"), XnaColor.White);
            }
        }

        public class CSpawnerEntityConverter
            : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return base.GetProperties(context, value, attributes);
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return base.GetPropertiesSupported(context);
            }
        }


        [TypeConverter(typeof(CSpawnerEntity))]
        public class CEditorSpawnerEntityConverter
            : TypeConverter
        {
            [Browsable(true), NotifyParentProperty(true), EditorBrowsable(EditorBrowsableState.Always)]
            public string Foo { get; set; }

            public override bool CanConvertFrom(ITypeDescriptorContext context,
               Type sourceType)
            {
                //if (sourceType == typeof(string))
                //{
                    //return true;
                //}
                Console.WriteLine(String.Format("CanConvert(): {0}", sourceType.ToString()));
                return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        /* custom editor
        class StringEditor : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                IWindowsFormsEditorService svc = (IWindowsFormsEditorService)
                    provider.GetService(typeof(IWindowsFormsEditorService));
                if (svc != null)
                {
                    svc.ShowDialog(new Form());
                    // update etc
                }
                return value;
            }
        }
         */
    }
}
