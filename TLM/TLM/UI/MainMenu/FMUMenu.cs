#define QUEUEDSTATSx

using System;
using System.Linq;
using ColossalFramework;
using ColossalFramework.UI;
using TrafficManager.Geometry;
using TrafficManager.TrafficLight;
using UnityEngine;
using TrafficManager.State;
using TrafficManager.Custom.PathFinding;
using System.Collections.Generic;
using TrafficManager.Manager;
using CSUtil.Commons;
using TrafficManager.Manager.Impl;
using TrafficManager.Util;
using CSUtil.Commons.Benchmark;

namespace TrafficManager.UI
{
#if DEBUG
    public class FMUMenuPanel : UIPanel
    {

        private static UIButton _FMUButton = null;
        private static ColossalFramework.UI.UIGraph _FMUgraph;

        public UIDragHandle Drag { get; private set; }

        public static UILabel title;


        //					int num7 = Singleton<ElectricityManager>.instance.TryFetchElectricity(data.m_position, num6, num4);
        //num = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetElectricityCapacity();
        //num2 = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetElectricityConsumption();

        public override void Start()
        {
            isVisible = false;

            backgroundSprite = "GenericPanel";
            color = new Color32(75, 75, 135, 255);
            width = Translation.getMenuWidth();
            height = 30;

            Vector2 resolution = UIView.GetAView().GetScreenResolution();
            relativePosition = new Vector3(50f, 75f);

            title = AddUIComponent<UILabel>();
            title.text = "FMU menu";
            title.relativePosition = new Vector3(50.0f, 5.0f);

            int y = 30;

            _FMUButton = _createButton("FMU", y, clickFMU);
            y += 40;
            height += 40;



            y += 100;
            height += 100;

            _FMUgraph = _createGraph("FMU", y);

            var dragHandler = new GameObject("FMU_Menu_DragHandler");
            dragHandler.transform.parent = transform;
            dragHandler.transform.localPosition = Vector3.zero;
            Drag = dragHandler.AddComponent<UIDragHandle>();
            Drag.enabled = true;
        }

        private UITextField CreateTextField(string str, int y)
        {
            UITextField textfield = AddUIComponent<UITextField>();
            textfield.relativePosition = new Vector3(15f, y);
            textfield.horizontalAlignment = UIHorizontalAlignment.Left;
            textfield.text = str;
            textfield.textScale = 0.8f;
            textfield.color = Color.black;
            textfield.cursorBlinkTime = 0.45f;
            textfield.cursorWidth = 1;
            textfield.selectionBackgroundColor = new Color(233, 201, 148, 255);
            textfield.selectionSprite = "EmptySprite";
            textfield.verticalAlignment = UIVerticalAlignment.Middle;
            textfield.padding = new RectOffset(5, 0, 5, 0);
            textfield.foregroundSpriteMode = UIForegroundSpriteMode.Fill;
            textfield.normalBgSprite = "TextFieldPanel";
            textfield.hoveredBgSprite = "TextFieldPanelHovered";
            textfield.focusedBgSprite = "TextFieldPanel";
            textfield.size = new Vector3(190, 30);
            textfield.isInteractive = true;
            textfield.enabled = true;
            textfield.readOnly = false;
            textfield.builtinKeyNavigation = true;
            textfield.width = Translation.getMenuWidth() - 30;
            return textfield;
        }

        private UIButton _createButton(string text, int y, MouseEventHandler eventClick)
        {
            var button = AddUIComponent<UIButton>();
            button.textScale = 0.8f;
            button.width = Translation.getMenuWidth() - 30;
            button.height = 30;
            button.normalBgSprite = "ButtonMenu";
            button.disabledBgSprite = "ButtonMenuDisabled";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.focusedBgSprite = "ButtonMenu";
            button.pressedBgSprite = "ButtonMenuPressed";
            button.textColor = new Color32(255, 255, 255, 255);
            button.playAudioEvents = true;
            button.text = text;
            button.relativePosition = new Vector3(15f, y);
            button.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                eventClick(component, eventParam);
                button.Invalidate();
            };

            return button;
        }

        private UIGraph m_FMUgraph;
        private UIGraph _createGraph(string text, int y)
        {
            m_FMUgraph = AddUIComponent<UIGraph>();
            m_FMUgraph.name = "fmuGraph";
            m_FMUgraph.relativePosition = new Vector3(15f, y);
            m_FMUgraph.width = 100;
            m_FMUgraph.height = 100;
            m_FMUgraph.Clear();
            m_FMUgraph.StartTime = DateTime.Now;
            m_FMUgraph.EndTime = DateTime.Now.AddMinutes(10);
            float[] array9 = new float[] { 1, 2, 3, 6, 5, 4, 1, 2, 3 };

            m_FMUgraph.AddCurve("stringUserData", "localID", array9, 1f, color, float.NegativeInfinity);
            m_FMUgraph.Start();
            m_FMUgraph.isVisible = true;
            m_FMUgraph.color = new Color32(150, 150, 150, 150);
            m_FMUgraph.enabled = true;
            m_FMUgraph.Show();
            m_FMUgraph.spriteName = "toto";
            m_FMUgraph.Update();
            return m_FMUgraph;
        }

        private void clickFMU(UIComponent component, UIMouseEventParameter eventParam)
        {
            Constants.ServiceFactory.SimulationService.AddAction(() =>
            {
                BenchmarkProfileProvider.Instance.ClearProfiles();
            });
        }

        //        public override void Update()
        //        {
        //#if QUEUEDSTATS
        //            if (showPathFindStats && title != null)
        //            {
        //                title.text = CustomPathManager.TotalQueuedPathFinds.ToString();
        //            }
        //#endif
        //        }
    }
#endif
}
