﻿//
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
        public List<Type> Types;

        public Type Result = typeof(CEntity);

        public CTypeSelectorControl(List<Type> types)
        {
            Types = types;
            InitializeComponent();
            this.Types = types;
            this.BorderStyle = BorderStyle.None;
            Result = typeof(CEntity);
        }

        private void InitializeComponent()
        {
            foreach (Type type in Types)
            {
                string typename = type.ToString().Substring("Galaxy.".Length);
                this.Items.Add(typename);
            }
        }
    }

    public class CEntityTypes
    {
        public static List<Type> Types = new List<Type>()
        {
            typeof(CBonus),
            typeof(CPowerup),
            typeof(CHealthBar),
        };
    }

    public class CEnemyTypes
    {
        public static List<Type> Types = new List<Type>()
        {
            typeof(CAirship),
            typeof(CBall),
            typeof(CBeamer),
            typeof(CBigBall),
            typeof(CBoss1),
            typeof(CBoss2),
            typeof(CBoss3),
            typeof(CBoss4),
            typeof(CBoss5),
            typeof(CBoss6),
            typeof(CBoss7),
            typeof(CBoss8),
            typeof(CBoss9),
            typeof(CBoss10),
            typeof(CBoss11),
            typeof(CBoss12),
            typeof(CBlackHole),
            typeof(CBonusShip),
            typeof(CCannon),
            typeof(CCutter),
            typeof(CDoubleTurret),
            typeof(CDownTurret),
            typeof(CFence),
            typeof(CGlob),
            typeof(CIsosceles),
            typeof(CMiniSquare),
            typeof(CMissilePod),
            typeof(CMissileTriple),
            typeof(CMissileStorm),
            typeof(CPyramid),
            typeof(CRaidTurret),
            typeof(CRotateTurret),
            typeof(CShootBall),
            typeof(CSpike),
            typeof(CSplitter),
            typeof(CSwirl),
            typeof(CTeleporter),
            typeof(CTurret),
            typeof(CTripleShot),
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
            FieldInfo field = typeof(Types).GetField("Types", BindingFlags.Static | BindingFlags.Public);
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
