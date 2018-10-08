using System;
using System.Collections.Generic;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.TestDataGenerator
{
    public partial class TestData
    {
        #region Group

        public ParameterGroup ParameterGroupProductType;
        public ParameterGroup ParameterGroupZip;
        public ParameterGroup ParameterGroupEqType;
        public ParameterGroup ParameterGroupBreakerType;
        public ParameterGroup ParameterGroupTransformatorType;
        public ParameterGroup ParameterGroupTransformatorCurrentType;
        public ParameterGroup ParameterGroupTVGType;
        public ParameterGroup ParameterGroupVoltage;
        public ParameterGroup ParameterGroupDrivesVoltage;
        public ParameterGroup ParameterGroupDrivesCurrentDisconnectors;
        public ParameterGroup ParameterGroupIsolation;
        public ParameterGroup ParameterGroupIsolationMaterial;
        public ParameterGroup ParameterGroupAccuracy;
        public ParameterGroup ParameterGroupCurrent;
        public ParameterGroup ParameterGroupCurrentBreaking;
        public ParameterGroup ParameterGroupNewProductDesignation;
        public ParameterGroup ParameterGroupDrives;
        public ParameterGroup ParameterGroupClimat;
        public ParameterGroup ParameterGroupPartType;
        public ParameterGroup ParameterGroupTransformersBlockStandartNumber;
        public ParameterGroup ParameterGroupTransformersBlockType;
        public ParameterGroup ParameterGroupTransformersBlockTarget;
        public ParameterGroup ParameterGroupServiceType;
        public ParameterGroup ParameterGroupControlCircuitVoltage;
        public ParameterGroup ParameterGroupTransformerLoad;
        public ParameterGroup ParameterGroupTransformerLimitMultiplicity;
        public ParameterGroup ParameterGroupTransformerSafetyK;
        public ParameterGroup ParameterGroupTransformerPrimaryCurrentRow;
        public ParameterGroup ParameterGroupTransformerSecondaryCurrent;
        public ParameterGroup ParameterGroupBreakerPhases;
        public ParameterGroup ParameterGroupBreakerLiveTankBreaks;

        private void GenerateParameterGroups()
        {
            ParameterGroupProductType.Clone(new ParameterGroup { Name = "Тип продукта" });
            ParameterGroupEqType.Clone(new ParameterGroup { Name = "Тип оборудования" });
            ParameterGroupZip.Clone(new ParameterGroup { Name = "Тип ЗИП" });
            ParameterGroupBreakerType.Clone(new ParameterGroup { Name = "Тип выключателя" });
            ParameterGroupTransformatorType.Clone(new ParameterGroup { Name = "Тип трансформатора" });
            ParameterGroupTransformatorCurrentType.Clone(new ParameterGroup { Name = "Установка ТТ" });
            ParameterGroupVoltage.Clone(new ParameterGroup { Name = "Номинальное напряжение", Measure = MeasureKv });
            ParameterGroupDrivesVoltage.Clone(new ParameterGroup { Name = "Номинальное напряжение двигателя завода пружин", Measure = MeasureKv });
            ParameterGroupIsolation.Clone(new ParameterGroup { Name = "Длина пути утечки" });
            ParameterGroupIsolationMaterial.Clone(new ParameterGroup { Name = "Тип изоляции" });
            ParameterGroupAccuracy.Clone(new ParameterGroup { Name = "Класс точности" });
            ParameterGroupCurrent.Clone(new ParameterGroup { Name = "Номинальный ток" });
            ParameterGroupCurrentBreaking.Clone(new ParameterGroup { Name = "Номинальный ток отключения" });
            ParameterGroupNewProductDesignation.Clone(new ParameterGroup { Name = "Обозначение" });
            ParameterGroupDrives.Clone(new ParameterGroup { Name = "Приводы" });
            ParameterGroupClimat.Clone(new ParameterGroup { Name = "Климатическое исполнение" });
            ParameterGroupPartType.Clone(new ParameterGroup { Name = "Тип составной части" });
            ParameterGroupTransformersBlockType.Clone(new ParameterGroup { Name = "Тип комплекта ТТ" });
            ParameterGroupTransformersBlockTarget.Clone(new ParameterGroup { Name = "Назначение комплекта ТТ" });
            ParameterGroupTransformersBlockStandartNumber.Clone(new ParameterGroup { Name = "Номер стандартного комплекта ТТ" });
            ParameterGroupServiceType.Clone(new ParameterGroup { Name = "Тип услуги" });
            ParameterGroupTVGType.Clone(new ParameterGroup { Name = "Тип встроенного ТТ" });
            ParameterGroupControlCircuitVoltage.Clone(new ParameterGroup { Name = "Напряжение цепей управления" });
            ParameterGroupTransformerLoad.Clone(new ParameterGroup { Name = "Нагрузка, ВА" });
            ParameterGroupTransformerLimitMultiplicity.Clone(new ParameterGroup { Name = "Предельная кратность" });
            ParameterGroupTransformerSafetyK.Clone(new ParameterGroup { Name = "Коэффициент безопасности" });
            ParameterGroupTransformerPrimaryCurrentRow.Clone(new ParameterGroup { Name = "Номинальные токи отпаек" });
            ParameterGroupTransformerSecondaryCurrent.Clone(new ParameterGroup { Name = "Номинальный вторичный ток" });
            ParameterGroupDrivesCurrentDisconnectors.Clone(new ParameterGroup { Name = "Установка двух токовых расцепителей" });
            ParameterGroupBreakerPhases.Clone(new ParameterGroup { Name = "Исполнение выключателя" });
            ParameterGroupBreakerLiveTankBreaks.Clone(new ParameterGroup { Name = "Исполнение по количеству разрывов" });
            
        }

        #endregion

        #region Parameter

        #region Параметры

        #region Тип продукта

        public Parameter ParameterNewProduct;
        public Parameter ParameterMainEquipment;
        public Parameter ParameterDependentEquipment;
        public Parameter ParameterService;        

        #endregion

        #region Тип услуги

        public Parameter ParameterSheffMontag;
        public Parameter ParameterDelivery;

        #endregion

        #region ЗИПы

        public Parameter ParameterZip1;
        public Parameter ParameterZip2;

        #endregion

        #region Тип основного оборудования

        public Parameter ParameterBreaker;
        public Parameter ParameterTransformer;
        public Parameter ParameterDisconnector;
        public Parameter ParameterEarthingSwitch;
        public Parameter ParameterProductParts;
        public Parameter ParameterKtpb;

        #endregion

        #region Составные части основоного оборудования

        public Parameter ParameterPartDrive;
        public Parameter ParameterPartTransformer;
        public Parameter ParameterPartTransformersBlock;

        #endregion

        #region Тип привода

        public Parameter ParameterDrivePPrK;
        public Parameter ParameterDrivePPV;
        public Parameter ParameterDriveDisconnector;
        public Parameter ParameterDrivePem;

        #endregion

        #region Тип выключателя

        public Parameter ParameterBreakerDeadTank;
        public Parameter ParameterBreakerLiveTank;

        #endregion

        #region Тип трансформатора

        public Parameter ParameterTransformerCurrent;
        public Parameter ParameterTransformerVoltage;

        #endregion

        #region Тип трансформатора тока

        public Parameter ParameterTransformerBuiltOut;
        public Parameter ParameterTransformerBuiltIn;

        #endregion

        #region Номинальное напряжение

        public Parameter ParameterVoltage35kV;
        public Parameter ParameterVoltage110kV;
        public Parameter ParameterVoltage220kV;
        public Parameter ParameterVoltage500kV;

        #endregion

        #region Номинальный ток

        public Parameter ParameterCurrent0630;
        public Parameter ParameterCurrent1000;
        public Parameter ParameterCurrent2500;
        public Parameter ParameterCurrent3150;
        public Parameter ParameterCurrent4000;

        #endregion

        #region Номинальный ток отключения

        public Parameter ParameterCurrentBreaking12kA;
        public Parameter ParameterCurrentBreaking40kA;
        public Parameter ParameterCurrentBreaking50kA;

        #endregion

        #region Климматическое исполнение

        public Parameter ParameterClimatT1;
        public Parameter ParameterClimatU1z;
        public Parameter ParameterClimatUHL1z;
        public Parameter ParameterClimatUHL1;
        public Parameter ParameterClimatU1;
        public Parameter ParameterClimatHL1z;

        #endregion

        #region Характеристики привода выключателя

        #region Токовые расцепители

        public Parameter ParameterDrivesCurrentDiscNo;
        public Parameter ParameterDrivesCurrentDisc3A;
        public Parameter ParameterDrivesCurrentDisc5A;

        #endregion

        #region Напряжение двигателя завода пружин

        public Parameter ParameterDrivesVoltage400V;
        public Parameter ParameterDrivesVoltage230V;
        public Parameter ParameterDrivesVoltage220V;
        public Parameter ParameterDrivesVoltage110V;

        #endregion

        #region Напряжение цепей управления

        public Parameter ParameterControlCircuitVoltage110V;
        public Parameter ParameterControlCircuitVoltage220V;

        #endregion

        #endregion

        #region Длина пути утечки

        public Parameter ParameterDpu2;
        public Parameter ParameterDpu3;
        public Parameter ParameterDpu4;

        #endregion

        #region Тип изоляции

        public Parameter ParameterFarfor;
        public Parameter ParameterPolimer;

        #endregion

        #region Трансформаторы тока

        #region Первичные токи отпаек ТТ

        public Parameter ParameterTransformerPrimaryCurrentRow1;
        public Parameter ParameterTransformerPrimaryCurrentRow2;

        #endregion

        #region Вторичный ток ТТ

        public Parameter ParameterTransformerSecondaryCurrent1;
        public Parameter ParameterTransformerSecondaryCurrent5;

        #endregion

        #region Класс точности ТТ

        public Parameter ParameterAccuracy02;
        public Parameter ParameterAccuracy02S;
        public Parameter ParameterAccuracy05;
        public Parameter ParameterAccuracy05S;
        public Parameter ParameterAccuracy05P;
        public Parameter ParameterAccuracy10P;

        #endregion

        #region Нагрузка ТТ

        public Parameter ParameterTransformerLoad05;
        public Parameter ParameterTransformerLoad10;
        public Parameter ParameterTransformerLoad15;
        public Parameter ParameterTransformerLoad20;
        public Parameter ParameterTransformerLoad25;
        public Parameter ParameterTransformerLoad30;
        public Parameter ParameterTransformerLoad35;
        public Parameter ParameterTransformerLoad40;
        public Parameter ParameterTransformerLoad45;
        public Parameter ParameterTransformerLoad50;
        public Parameter ParameterTransformerLoad55;
        public Parameter ParameterTransformerLoad60;

        #endregion

        #region Коэффициент предельной кратности ТТ

        public Parameter ParameterTransformerLimitMultiplicity05;
        public Parameter ParameterTransformerLimitMultiplicity10;
        public Parameter ParameterTransformerLimitMultiplicity15;
        public Parameter ParameterTransformerLimitMultiplicity20;
        public Parameter ParameterTransformerLimitMultiplicity25;
        public Parameter ParameterTransformerLimitMultiplicity30;
        public Parameter ParameterTransformerLimitMultiplicity35;
        public Parameter ParameterTransformerLimitMultiplicity40;
        public Parameter ParameterTransformerLimitMultiplicity45;
        public Parameter ParameterTransformerLimitMultiplicity50;
        public Parameter ParameterTransformerLimitMultiplicity55;
        public Parameter ParameterTransformerLimitMultiplicity60;

        #endregion

        #region Коэффициент безопасности ТТ

        public Parameter ParameterTransformerSafetyK02;
        public Parameter ParameterTransformerSafetyK04;
        public Parameter ParameterTransformerSafetyK06;
        public Parameter ParameterTransformerSafetyK08;
        public Parameter ParameterTransformerSafetyK10;
        public Parameter ParameterTransformerSafetyK12;
        public Parameter ParameterTransformerSafetyK14;
        public Parameter ParameterTransformerSafetyK16;
        public Parameter ParameterTransformerSafetyK18;
        public Parameter ParameterTransformerSafetyK20;
        public Parameter ParameterTransformerSafetyK22;
        public Parameter ParameterTransformerSafetyK24;

        #endregion

        #region Тип комплекта ТТ

        public Parameter ParameterTransformersBlockTypeStandart;
        public Parameter ParameterTransformersBlockTypeCustom;

        #endregion

        #region Стандартные комплекты ТТ

        public Parameter ParameterTransformersBlockStandartVeb110Num1;
        public Parameter ParameterTransformersBlockStandartVeb110Num2;
        public Parameter ParameterTransformersBlockStandartVeb220Num1;
        public Parameter ParameterTransformersBlockStandartVeb220Num2;

        #endregion

        #region Назначение ТТ

        public Parameter ParameterTransformersBlockTargetVeb110;
        public Parameter ParameterTransformersBlockTargetVeb220;
        public Parameter ParameterTransformersBlockTargetVgb35;

        #endregion        

        #endregion

        #region Исполнение выключателя по полюсности

        public Parameter ParameterBreakerPhases3;
        public Parameter ParameterBreakerPhases1;

        #endregion

        #region Исполнение выключателя по количеству разрывов

        public Parameter ParameterBreakerLiveTankBreaks1Razriv;

        #endregion

        #endregion

        private void GenerateParameters()
        {
                    
            #region Тип продукта

            ParameterNewProduct.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Оборудование новое" });
            ParameterMainEquipment.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Оборудование главное" });
            ParameterDependentEquipment.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Оборудование дополнительное" });
            ParameterService.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Услуга" });
            ParameterProductParts.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Составные части оборудования" });

            #endregion

            #region Тип услуги

            ParameterSheffMontag.Clone(new Parameter { ParameterGroup = ParameterGroupServiceType, Value = "Шеф-монтаж" });
            ParameterDelivery.Clone(new Parameter { ParameterGroup = ParameterGroupServiceType, Value = "Доставка" });

            ParameterSheffMontag.AddRequiredPreviousParameters(ParameterService);
            ParameterDelivery.AddRequiredPreviousParameters(ParameterService);

            #endregion

            #region ЗИПы

            ParameterZip1.Clone(new Parameter { ParameterGroup = ParameterGroupZip, Value = "Групповой комплект ЗИП №1 (газотехнология)" });
            ParameterZip2.Clone(new Parameter { ParameterGroup = ParameterGroupZip, Value = "Групповой комплект ЗИП №2 (элегаз)" });

            ParameterZip1.AddRequiredPreviousParameters(ParameterDependentEquipment);
            ParameterZip2.AddRequiredPreviousParameters(ParameterDependentEquipment);

            #endregion

            #region Тип основного оборудования

            ParameterBreaker.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "Выключатель" });
            ParameterTransformer.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "Трансформатор" });
            ParameterDisconnector.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "Разъединитель" });
            ParameterEarthingSwitch.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "Заземлитель" });
            ParameterKtpb.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "КТПБ" });

            ParameterBreaker.AddRequiredPreviousParameters(ParameterMainEquipment);
            ParameterDisconnector.AddRequiredPreviousParameters(ParameterMainEquipment);
            ParameterEarthingSwitch.AddRequiredPreviousParameters(ParameterMainEquipment);
            ParameterKtpb.AddRequiredPreviousParameters(ParameterMainEquipment);
            ParameterTransformer.AddRequiredPreviousParameters(ParameterMainEquipment)
                                .AddRequiredPreviousParameters(ParameterPartTransformer);

            #endregion

            #region Составные части основоного оборудования

            ParameterPartDrive.Clone(new Parameter { ParameterGroup = ParameterGroupPartType, Value = "Привод" });
            ParameterPartTransformer.Clone(new Parameter { ParameterGroup = ParameterGroupPartType, Value = "Трансформатор" });
            ParameterPartTransformersBlock.Clone(new Parameter { ParameterGroup = ParameterGroupPartType, Value = "Блок трансформаторов" });

            ParameterPartDrive.AddRequiredPreviousParameters(ParameterProductParts);
            ParameterPartTransformer.AddRequiredPreviousParameters(ParameterProductParts);
            ParameterPartTransformersBlock.AddRequiredPreviousParameters(ParameterProductParts);

            #endregion

            #region Тип привода

            ParameterDrivePem.Clone(new Parameter { ParameterGroup = ParameterGroupDrives, Value = "Привод ПЭМ" });
            ParameterDrivePPrK.Clone(new Parameter { ParameterGroup = ParameterGroupDrives, Value = "Привод ППрК" });
            ParameterDrivePPV.Clone(new Parameter { ParameterGroup = ParameterGroupDrives, Value = "Привод ППВ" });
            ParameterDriveDisconnector.Clone(new Parameter { ParameterGroup = ParameterGroupDrives, Value = "Привод разъединителя/заземлителя" });

            ParameterDrivePem.AddRequiredPreviousParameters(ParameterPartDrive);
            ParameterDrivePPrK.AddRequiredPreviousParameters(ParameterPartDrive);
            ParameterDrivePPV.AddRequiredPreviousParameters(ParameterPartDrive);
            ParameterDriveDisconnector.AddRequiredPreviousParameters(ParameterPartDrive);

            #endregion

            #region Характеристики привода выключателя

            #region Токовые расцепители

            ParameterDrivesCurrentDiscNo.Clone(new Parameter { ParameterGroup = ParameterGroupDrivesCurrentDisconnectors, Value = "нет" });
            ParameterDrivesCurrentDisc3A.Clone(new Parameter { ParameterGroup = ParameterGroupDrivesCurrentDisconnectors, Value = "на 3 А" });
            ParameterDrivesCurrentDisc5A.Clone(new Parameter { ParameterGroup = ParameterGroupDrivesCurrentDisconnectors, Value = "на 5 А" });

            ParameterDrivesCurrentDiscNo.AddRequiredPreviousParameters(ParameterDrivePPrK);
            ParameterDrivesCurrentDisc3A.AddRequiredPreviousParameters(ParameterDrivePPrK);
            ParameterDrivesCurrentDisc5A.AddRequiredPreviousParameters(ParameterDrivePPrK);
            
            #endregion

            #region Напряжение двигателя завода пружин

            ParameterDrivesVoltage400V.Clone(new Parameter { ParameterGroup = ParameterGroupDrivesVoltage, Value = "~400 В (3ф. звезда)" });
            ParameterDrivesVoltage230V.Clone(new Parameter { ParameterGroup = ParameterGroupDrivesVoltage, Value = "~230 В (3ф. треугольник)" });
            ParameterDrivesVoltage220V.Clone(new Parameter { ParameterGroup = ParameterGroupDrivesVoltage, Value = "~/=220 В (1ф.)" });
            ParameterDrivesVoltage110V.Clone(new Parameter { ParameterGroup = ParameterGroupDrivesVoltage, Value = "=110 В" });

            ParameterDrivesVoltage400V.AddRequiredPreviousParameters(ParameterDrivePPrK).AddRequiredPreviousParameters(ParameterDrivePPV);
            ParameterDrivesVoltage230V.AddRequiredPreviousParameters(ParameterDrivePPrK).AddRequiredPreviousParameters(ParameterDrivePPV);
            ParameterDrivesVoltage110V.AddRequiredPreviousParameters(ParameterDrivePPrK).AddRequiredPreviousParameters(ParameterDrivePPV);
            ParameterDrivesVoltage220V.AddRequiredPreviousParameters(ParameterDrivePPrK).AddRequiredPreviousParameters(ParameterDrivePPV);
            
            #endregion

            #region Напряжение цепей управления

            ParameterControlCircuitVoltage110V.Clone(new Parameter { ParameterGroup = ParameterGroupControlCircuitVoltage, Value = "= 110 В" });
            ParameterControlCircuitVoltage220V.Clone(new Parameter { ParameterGroup = ParameterGroupControlCircuitVoltage, Value = "= 220 В" });

            ParameterControlCircuitVoltage110V.AddRequiredPreviousParameters(ParameterDrivePPrK)
                                              .AddRequiredPreviousParameters(ParameterDrivePPV);
            ParameterControlCircuitVoltage220V.AddRequiredPreviousParameters(ParameterDrivePPrK)
                                              .AddRequiredPreviousParameters(ParameterDrivePPV);

            #endregion

            #endregion

            #region Тип выключателя

            ParameterBreakerDeadTank.Clone(new Parameter { ParameterGroup = ParameterGroupBreakerType, Value = "Баковый" });
            ParameterBreakerLiveTank.Clone(new Parameter { ParameterGroup = ParameterGroupBreakerType, Value = "Колонковый" });

            ParameterBreakerDeadTank.AddRequiredPreviousParameters(ParameterBreaker);
            ParameterBreakerLiveTank.AddRequiredPreviousParameters(ParameterBreaker);

            #endregion

            #region Тип трансформатора

            ParameterTransformerCurrent.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorType, Value = "Тока" });
            ParameterTransformerVoltage.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorType, Value = "Напряжения" });

            ParameterTransformerCurrent.AddRequiredPreviousParameters(ParameterTransformer);
            ParameterTransformerVoltage.AddRequiredPreviousParameters(ParameterTransformer, ParameterMainEquipment);

            #endregion

            #region Тип трансформатора тока

            ParameterTransformerBuiltOut.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorCurrentType, Value = "Отдельностоящий" });
            ParameterTransformerBuiltIn.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorCurrentType, Value = "Встроенный" });

            ParameterTransformerBuiltOut.AddRequiredPreviousParameters(ParameterTransformerCurrent, ParameterMainEquipment);
            ParameterTransformerBuiltIn.AddRequiredPreviousParameters(ParameterTransformerCurrent, ParameterPartTransformer);
            
            #endregion

            #region Номинальное напряжение

            ParameterVoltage35kV.Clone(new Parameter { ParameterGroup = ParameterGroupVoltage, Value = "35 кВ" });
            ParameterVoltage110kV.Clone(new Parameter { ParameterGroup = ParameterGroupVoltage, Value = "110 кВ" });
            ParameterVoltage220kV.Clone(new Parameter { ParameterGroup = ParameterGroupVoltage, Value = "220 кВ" });
            ParameterVoltage500kV.Clone(new Parameter { ParameterGroup = ParameterGroupVoltage, Value = "500 кВ" });

            ParameterVoltage35kV.AddRequiredPreviousParameters(ParameterBreaker)
                                .AddRequiredPreviousParameters(ParameterTransformerCurrent);
            ParameterVoltage110kV.AddRequiredPreviousParameters(ParameterBreaker)
                                 .AddRequiredPreviousParameters(ParameterTransformer)
                                 .AddRequiredPreviousParameters(ParameterDisconnector)
                                 .AddRequiredPreviousParameters(ParameterEarthingSwitch);
            ParameterVoltage220kV.AddRequiredPreviousParameters(ParameterBreaker)
                                 .AddRequiredPreviousParameters(ParameterTransformer)
                                 .AddRequiredPreviousParameters(ParameterDisconnector)
                                 .AddRequiredPreviousParameters(ParameterEarthingSwitch);
            ParameterVoltage500kV.AddRequiredPreviousParameters(ParameterBreakerLiveTank);

            #endregion

            #region Номинальный ток

            ParameterCurrent0630.Clone(new Parameter { ParameterGroup = ParameterGroupCurrent, Value = "630 А" });
            ParameterCurrent1000.Clone(new Parameter { ParameterGroup = ParameterGroupCurrent, Value = "1000 А" });
            ParameterCurrent2500.Clone(new Parameter { ParameterGroup = ParameterGroupCurrent, Value = "2500 А" });
            ParameterCurrent3150.Clone(new Parameter { ParameterGroup = ParameterGroupCurrent, Value = "3150 А" });
            ParameterCurrent4000.Clone(new Parameter { ParameterGroup = ParameterGroupCurrent, Value = "4000 А" });

            ParameterCurrent0630.AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage35kV);
            ParameterCurrent1000.AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage35kV);

            ParameterCurrent2500.AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage110kV)
                                .AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage220kV, ParameterBreakerDeadTank);
            ParameterCurrent3150.AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage110kV)
                                .AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage220kV);

            ParameterCurrent4000.AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage500kV);

            #endregion

            #region Номинальный ток

            ParameterCurrentBreaking12kA.Clone(new Parameter { ParameterGroup = ParameterGroupCurrentBreaking, Value = "12,5 кА" });
            ParameterCurrentBreaking40kA.Clone(new Parameter { ParameterGroup = ParameterGroupCurrentBreaking, Value = "40 кА" });
            ParameterCurrentBreaking50kA.Clone(new Parameter { ParameterGroup = ParameterGroupCurrentBreaking, Value = "50 кА" });

            ParameterCurrentBreaking12kA.AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage35kV);
            ParameterCurrentBreaking40kA.AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage110kV)
                                        .AddRequiredPreviousParameters(ParameterBreakerLiveTank, ParameterVoltage500kV);
            ParameterCurrentBreaking50kA.AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage110kV)
                                        .AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage220kV)
                                        .AddRequiredPreviousParameters(ParameterBreakerLiveTank, ParameterVoltage35kV);

            #endregion

            #region Климматическое исполнение

            ParameterClimatT1.Clone(new Parameter { ParameterGroup = ParameterGroupClimat, Value = "T1 (от +50 до -40)" });
            ParameterClimatU1z.Clone(new Parameter { ParameterGroup = ParameterGroupClimat, Value = "У1* (от +40 до -40)" });
            ParameterClimatUHL1z.Clone(new Parameter { ParameterGroup = ParameterGroupClimat, Value = "УХЛ1* (от +40 до -55)" });
            ParameterClimatUHL1.Clone(new Parameter { ParameterGroup = ParameterGroupClimat, Value = "УХЛ1 (от +40 до -60)" });
            ParameterClimatU1.Clone(new Parameter { ParameterGroup = ParameterGroupClimat, Value = "У1 (от +40 до -45)" });
            ParameterClimatHL1z.Clone(new Parameter { ParameterGroup = ParameterGroupClimat, Value = "ХЛ1* (от +40 до -55)" });

            ParameterClimatT1.AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage220kV);
            ParameterClimatU1z.AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage110kV);
            ParameterClimatUHL1z.AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage110kV);
            ParameterClimatUHL1.AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage110kV);
            ParameterClimatU1.AddRequiredPreviousParameters(ParameterBreakerLiveTank, ParameterVoltage110kV);
            ParameterClimatHL1z.AddRequiredPreviousParameters(ParameterBreakerLiveTank, ParameterVoltage110kV);

            #endregion

            #region Длина пути утечки

            ParameterDpu2.Clone(new Parameter { ParameterGroup = ParameterGroupIsolation, Value = "II*" });
            ParameterDpu3.Clone(new Parameter { ParameterGroup = ParameterGroupIsolation, Value = "III" });
            ParameterDpu4.Clone(new Parameter { ParameterGroup = ParameterGroupIsolation, Value = "IV" });

            ParameterDpu2.AddRequiredPreviousParameters(ParameterFarfor);
            ParameterDpu3.AddRequiredPreviousParameters(ParameterFarfor);
            ParameterDpu4.AddRequiredPreviousParameters(ParameterFarfor)
                         .AddRequiredPreviousParameters(ParameterPolimer);

            #endregion

            #region Тип изоляции

            ParameterFarfor.Clone(new Parameter { ParameterGroup = ParameterGroupIsolationMaterial, Value = "Фарфор" });
            ParameterPolimer.Clone(new Parameter { ParameterGroup = ParameterGroupIsolationMaterial, Value = "Полимер" });

            ParameterFarfor.AddRequiredPreviousParameters(ParameterBreaker)
                           .AddRequiredPreviousParameters(ParameterDisconnector)
                           .AddRequiredPreviousParameters(ParameterEarthingSwitch)
                           .AddRequiredPreviousParameters(ParameterTransformerVoltage)
                           .AddRequiredPreviousParameters(ParameterTransformerCurrent, ParameterTransformerBuiltOut);

            ParameterPolimer.AddRequiredPreviousParameters(ParameterBreaker)
                            .AddRequiredPreviousParameters(ParameterDisconnector)
                            .AddRequiredPreviousParameters(ParameterEarthingSwitch)
                            .AddRequiredPreviousParameters(ParameterTransformerVoltage)
                            .AddRequiredPreviousParameters(ParameterTransformerCurrent, ParameterTransformerBuiltOut);
            
            #endregion

            #region Трансформаторы тока

            #region Первичные токи отпаек ТТ

            ParameterTransformerPrimaryCurrentRow1.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerPrimaryCurrentRow, Value = "600-400-300-200 А" });
            ParameterTransformerPrimaryCurrentRow2.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerPrimaryCurrentRow, Value = "2000-1500-1000-500 А" });

            ParameterTransformerPrimaryCurrentRow1.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerPrimaryCurrentRow2.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);

            #endregion

            #region Вторичный ток ТТ

            ParameterTransformerSecondaryCurrent1.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSecondaryCurrent, Value = "1 А" });
            ParameterTransformerSecondaryCurrent5.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSecondaryCurrent, Value = "5 А" });

            ParameterTransformerSecondaryCurrent1.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerSecondaryCurrent5.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);

            #endregion

            #region Класс точности ТТ

            ParameterAccuracy02.Clone(new Parameter { ParameterGroup = ParameterGroupAccuracy, Value = "0,2" });
            ParameterAccuracy02S.Clone(new Parameter { ParameterGroup = ParameterGroupAccuracy, Value = "0,2S" });
            ParameterAccuracy05.Clone(new Parameter { ParameterGroup = ParameterGroupAccuracy, Value = "0,5" });
            ParameterAccuracy05S.Clone(new Parameter { ParameterGroup = ParameterGroupAccuracy, Value = "0,5S" });
            ParameterAccuracy05P.Clone(new Parameter { ParameterGroup = ParameterGroupAccuracy, Value = "5P" });
            ParameterAccuracy10P.Clone(new Parameter { ParameterGroup = ParameterGroupAccuracy, Value = "10P" });

            ParameterAccuracy02.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterAccuracy02S.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterAccuracy05.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterAccuracy05S.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterAccuracy05P.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterAccuracy10P.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);

            #endregion

            #region Нагрузка ТТ

            ParameterTransformerLoad05.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLoad, Value = "5 ВА" });
            ParameterTransformerLoad10.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLoad, Value = "10 ВА" });
            ParameterTransformerLoad15.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLoad, Value = "15 ВА" });
            ParameterTransformerLoad20.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLoad, Value = "20 ВА" });
            ParameterTransformerLoad25.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLoad, Value = "25 ВА" });
            ParameterTransformerLoad30.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLoad, Value = "30 ВА" });
            ParameterTransformerLoad35.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLoad, Value = "35 ВА" });
            ParameterTransformerLoad40.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLoad, Value = "40 ВА" });
            ParameterTransformerLoad45.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLoad, Value = "45 ВА" });
            ParameterTransformerLoad50.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLoad, Value = "50 ВА" });
            ParameterTransformerLoad55.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLoad, Value = "55 ВА" });
            ParameterTransformerLoad60.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLoad, Value = "60 ВА" });

            ParameterTransformerLoad05.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerLoad10.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerLoad15.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerLoad20.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerLoad25.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerLoad30.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerLoad35.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerLoad40.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerLoad45.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerLoad50.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerLoad55.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterTransformerLoad60.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);

            #endregion

            #region Коэффициент предельной кратности ТТ

            ParameterTransformerLimitMultiplicity05.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLimitMultiplicity, Value = "5" });
            ParameterTransformerLimitMultiplicity10.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLimitMultiplicity, Value = "10" });
            ParameterTransformerLimitMultiplicity15.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLimitMultiplicity, Value = "15" });
            ParameterTransformerLimitMultiplicity20.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLimitMultiplicity, Value = "20" });
            ParameterTransformerLimitMultiplicity25.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLimitMultiplicity, Value = "25" });
            ParameterTransformerLimitMultiplicity30.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLimitMultiplicity, Value = "30" });
            ParameterTransformerLimitMultiplicity35.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLimitMultiplicity, Value = "35" });
            ParameterTransformerLimitMultiplicity40.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLimitMultiplicity, Value = "40" });
            ParameterTransformerLimitMultiplicity45.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLimitMultiplicity, Value = "45" });
            ParameterTransformerLimitMultiplicity50.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLimitMultiplicity, Value = "50" });
            ParameterTransformerLimitMultiplicity55.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLimitMultiplicity, Value = "55" });
            ParameterTransformerLimitMultiplicity60.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerLimitMultiplicity, Value = "60" });

            ParameterTransformerLimitMultiplicity05.AddRequiredPreviousParameters(ParameterAccuracy05P).AddRequiredPreviousParameters(ParameterAccuracy10P);
            ParameterTransformerLimitMultiplicity10.AddRequiredPreviousParameters(ParameterAccuracy05P).AddRequiredPreviousParameters(ParameterAccuracy10P);
            ParameterTransformerLimitMultiplicity15.AddRequiredPreviousParameters(ParameterAccuracy05P).AddRequiredPreviousParameters(ParameterAccuracy10P);
            ParameterTransformerLimitMultiplicity20.AddRequiredPreviousParameters(ParameterAccuracy05P).AddRequiredPreviousParameters(ParameterAccuracy10P);
            ParameterTransformerLimitMultiplicity25.AddRequiredPreviousParameters(ParameterAccuracy05P).AddRequiredPreviousParameters(ParameterAccuracy10P);
            ParameterTransformerLimitMultiplicity30.AddRequiredPreviousParameters(ParameterAccuracy05P).AddRequiredPreviousParameters(ParameterAccuracy10P);
            ParameterTransformerLimitMultiplicity35.AddRequiredPreviousParameters(ParameterAccuracy05P).AddRequiredPreviousParameters(ParameterAccuracy10P);
            ParameterTransformerLimitMultiplicity40.AddRequiredPreviousParameters(ParameterAccuracy05P).AddRequiredPreviousParameters(ParameterAccuracy10P);
            ParameterTransformerLimitMultiplicity45.AddRequiredPreviousParameters(ParameterAccuracy05P).AddRequiredPreviousParameters(ParameterAccuracy10P);
            ParameterTransformerLimitMultiplicity50.AddRequiredPreviousParameters(ParameterAccuracy05P).AddRequiredPreviousParameters(ParameterAccuracy10P);
            ParameterTransformerLimitMultiplicity55.AddRequiredPreviousParameters(ParameterAccuracy05P).AddRequiredPreviousParameters(ParameterAccuracy10P);
            ParameterTransformerLimitMultiplicity60.AddRequiredPreviousParameters(ParameterAccuracy05P).AddRequiredPreviousParameters(ParameterAccuracy10P);

            #endregion

            #region Коэффициент безопасности ТТ

            ParameterTransformerSafetyK02.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSafetyK, Value = "2" });
            ParameterTransformerSafetyK04.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSafetyK, Value = "4" });
            ParameterTransformerSafetyK06.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSafetyK, Value = "6" });
            ParameterTransformerSafetyK08.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSafetyK, Value = "8" });
            ParameterTransformerSafetyK10.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSafetyK, Value = "10" });
            ParameterTransformerSafetyK12.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSafetyK, Value = "12" });
            ParameterTransformerSafetyK14.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSafetyK, Value = "14" });
            ParameterTransformerSafetyK16.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSafetyK, Value = "16" });
            ParameterTransformerSafetyK18.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSafetyK, Value = "18" });
            ParameterTransformerSafetyK20.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSafetyK, Value = "20" });
            ParameterTransformerSafetyK22.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSafetyK, Value = "22" });
            ParameterTransformerSafetyK24.Clone(new Parameter { ParameterGroup = ParameterGroupTransformerSafetyK, Value = "24" });

            ParameterTransformerSafetyK02.AddRequiredPreviousParameters(ParameterAccuracy02).AddRequiredPreviousParameters(ParameterAccuracy02S).AddRequiredPreviousParameters(ParameterAccuracy05).AddRequiredPreviousParameters(ParameterAccuracy05S);
            ParameterTransformerSafetyK04.AddRequiredPreviousParameters(ParameterAccuracy02).AddRequiredPreviousParameters(ParameterAccuracy02S).AddRequiredPreviousParameters(ParameterAccuracy05).AddRequiredPreviousParameters(ParameterAccuracy05S);
            ParameterTransformerSafetyK06.AddRequiredPreviousParameters(ParameterAccuracy02).AddRequiredPreviousParameters(ParameterAccuracy02S).AddRequiredPreviousParameters(ParameterAccuracy05).AddRequiredPreviousParameters(ParameterAccuracy05S);
            ParameterTransformerSafetyK08.AddRequiredPreviousParameters(ParameterAccuracy02).AddRequiredPreviousParameters(ParameterAccuracy02S).AddRequiredPreviousParameters(ParameterAccuracy05).AddRequiredPreviousParameters(ParameterAccuracy05S);
            ParameterTransformerSafetyK10.AddRequiredPreviousParameters(ParameterAccuracy02).AddRequiredPreviousParameters(ParameterAccuracy02S).AddRequiredPreviousParameters(ParameterAccuracy05).AddRequiredPreviousParameters(ParameterAccuracy05S);
            ParameterTransformerSafetyK12.AddRequiredPreviousParameters(ParameterAccuracy02).AddRequiredPreviousParameters(ParameterAccuracy02S).AddRequiredPreviousParameters(ParameterAccuracy05).AddRequiredPreviousParameters(ParameterAccuracy05S);
            ParameterTransformerSafetyK14.AddRequiredPreviousParameters(ParameterAccuracy02).AddRequiredPreviousParameters(ParameterAccuracy02S).AddRequiredPreviousParameters(ParameterAccuracy05).AddRequiredPreviousParameters(ParameterAccuracy05S);
            ParameterTransformerSafetyK16.AddRequiredPreviousParameters(ParameterAccuracy02).AddRequiredPreviousParameters(ParameterAccuracy02S).AddRequiredPreviousParameters(ParameterAccuracy05).AddRequiredPreviousParameters(ParameterAccuracy05S);
            ParameterTransformerSafetyK18.AddRequiredPreviousParameters(ParameterAccuracy02).AddRequiredPreviousParameters(ParameterAccuracy02S).AddRequiredPreviousParameters(ParameterAccuracy05).AddRequiredPreviousParameters(ParameterAccuracy05S);
            ParameterTransformerSafetyK20.AddRequiredPreviousParameters(ParameterAccuracy02).AddRequiredPreviousParameters(ParameterAccuracy02S).AddRequiredPreviousParameters(ParameterAccuracy05).AddRequiredPreviousParameters(ParameterAccuracy05S);
            ParameterTransformerSafetyK22.AddRequiredPreviousParameters(ParameterAccuracy02).AddRequiredPreviousParameters(ParameterAccuracy02S).AddRequiredPreviousParameters(ParameterAccuracy05).AddRequiredPreviousParameters(ParameterAccuracy05S);
            ParameterTransformerSafetyK24.AddRequiredPreviousParameters(ParameterAccuracy02).AddRequiredPreviousParameters(ParameterAccuracy02S).AddRequiredPreviousParameters(ParameterAccuracy05).AddRequiredPreviousParameters(ParameterAccuracy05S);
            
            #endregion

            #region Тип комплекта ТТ

            ParameterTransformersBlockTypeStandart.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockType, Value = "Стандартный" });
            ParameterTransformersBlockTypeCustom.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockType, Value = "По заказу" });

            ParameterTransformersBlockTypeStandart.AddRequiredPreviousParameters(ParameterTransformersBlockTargetVeb110)
                                                  .AddRequiredPreviousParameters(ParameterTransformersBlockTargetVeb220)
                                                  .AddRequiredPreviousParameters(ParameterTransformersBlockTargetVgb35);
            ParameterTransformersBlockTypeCustom.AddRequiredPreviousParameters(ParameterTransformersBlockTargetVeb110)
                                                .AddRequiredPreviousParameters(ParameterTransformersBlockTargetVeb220)
                                                .AddRequiredPreviousParameters(ParameterTransformersBlockTargetVgb35);

            #endregion

            #region Стандартные комплекты ТТ

            ParameterTransformersBlockStandartVeb110Num1.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockStandartNumber, Value = "602-231 (300-200-150-100/5)" });
            ParameterTransformersBlockStandartVeb110Num2.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockStandartNumber, Value = "602-112 (600-400-300-200/5)" });
            ParameterTransformersBlockStandartVeb220Num1.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockStandartNumber, Value = "623-192 (2000-1500-1000-500/5)" });
            ParameterTransformersBlockStandartVeb220Num2.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockStandartNumber, Value = "623-194 (2000-1500-1000-500/1)" });

            ParameterTransformersBlockStandartVeb110Num1.AddRequiredPreviousParameters(ParameterTransformersBlockTargetVeb110, ParameterTransformersBlockTypeStandart);
            ParameterTransformersBlockStandartVeb110Num2.AddRequiredPreviousParameters(ParameterTransformersBlockTargetVeb110, ParameterTransformersBlockTypeStandart);
            ParameterTransformersBlockStandartVeb220Num1.AddRequiredPreviousParameters(ParameterTransformersBlockTargetVeb220, ParameterTransformersBlockTypeStandart);
            ParameterTransformersBlockStandartVeb220Num2.AddRequiredPreviousParameters(ParameterTransformersBlockTargetVeb220, ParameterTransformersBlockTypeStandart);
            
            #endregion

            #region Назначение ТТ

            ParameterTransformersBlockTargetVeb110.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockTarget, Value = "Для ВЭБ-110 (3 фазы)" });
            ParameterTransformersBlockTargetVeb220.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockTarget, Value = "Для ВЭБ-220 (3 фазы)" });
            ParameterTransformersBlockTargetVgb35.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockTarget, Value = "Для ВГБ-35 (3 фазы)" });

            ParameterTransformersBlockTargetVeb110.AddRequiredPreviousParameters(ParameterPartTransformersBlock);
            ParameterTransformersBlockTargetVeb220.AddRequiredPreviousParameters(ParameterPartTransformersBlock);
            ParameterTransformersBlockTargetVgb35.AddRequiredPreviousParameters(ParameterPartTransformersBlock);

            #endregion        

            #endregion

            #region Исполнение выключателя по полюсности

            ParameterBreakerPhases3.Clone(new Parameter { ParameterGroup = ParameterGroupBreakerPhases, Value = "Трехфазное", Rang = 10});
            ParameterBreakerPhases1.Clone(new Parameter { ParameterGroup = ParameterGroupBreakerPhases, Value = "Однофазное" });

            ParameterBreakerPhases3.AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage110kV).AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage220kV);
            ParameterBreakerPhases1.AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage110kV).AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage220kV);
            
            #endregion

            #region Исполнение выключателя по количеству разрывов

            ParameterBreakerLiveTankBreaks1Razriv.Clone(new Parameter { ParameterGroup = ParameterGroupBreakerLiveTankBreaks, Value = "Одноразрывный" });

            ParameterBreakerLiveTankBreaks1Razriv.AddRequiredPreviousParameters(ParameterBreakerLiveTank, ParameterVoltage220kV);
            
            #endregion

        }

        #endregion

        #region ProductType

        public ProductType ProductTypeDeadTankBreaker;
        public ProductType ProductTypeLiveTankBreaker;
        public ProductType ProductTypeCurrentTransformer;
        public ProductType ProductTypeVoltageTransformer;

        private void GenerateProductTypes()
        {
            ProductTypeDeadTankBreaker.Clone(new ProductType { Name = "Выключатель баковый" });
            ProductTypeLiveTankBreaker.Clone(new ProductType { Name = "Выключатель колонковый" });
            ProductTypeCurrentTransformer.Clone(new ProductType { Name = "Трансформатор тока" });
            ProductTypeVoltageTransformer.Clone(new ProductType { Name = "Трансформатор напряжения" });
        }

        public ProductTypeDesignation ProductTypeDesignationDeadTankBreaker;
        public ProductTypeDesignation ProductTypeDesignationLiveTankBreaker;
        public ProductTypeDesignation ProductTypeDesignationCurrentTransformer;
        public ProductTypeDesignation ProductTypeDesignationVoltageTransformer;

        private void GenerateProductTypeDesignations()
        {
            ProductTypeDesignationDeadTankBreaker.Clone(new ProductTypeDesignation { ProductType = ProductTypeDeadTankBreaker, Parameters = new List<Parameter> { ParameterBreaker, ParameterBreakerDeadTank } });
            ProductTypeDesignationLiveTankBreaker.Clone(new ProductTypeDesignation { ProductType = ProductTypeLiveTankBreaker, Parameters = new List<Parameter> { ParameterBreaker, ParameterBreakerLiveTank } });
            ProductTypeDesignationCurrentTransformer.Clone(new ProductTypeDesignation { ProductType = ProductTypeCurrentTransformer, Parameters = new List<Parameter> { ParameterTransformer, ParameterTransformerCurrent } });
            ProductTypeDesignationVoltageTransformer.Clone(new ProductTypeDesignation { ProductType = ProductTypeVoltageTransformer, Parameters = new List<Parameter> { ParameterTransformer, ParameterTransformerVoltage } });
        }

        #endregion

        #region ProductDesignation

        public ProductDesignation ProductDesignationVgb35;
        public ProductDesignation ProductDesignationVgt35;
        public ProductDesignation ProductDesignationVgt110;
        public ProductDesignation ProductDesignationVgt2201A1;

        public ProductDesignation ProductDesignationVeb110;
        public ProductDesignation ProductDesignationVeb110II;
        public ProductDesignation ProductDesignationVeb110III;
        public ProductDesignation ProductDesignationVeb110IV;
        public ProductDesignation ProductDesignationZng110;
        public ProductDesignation ProductDesignationVeb220;
        public ProductDesignation ProductDesignationZng220;
        public ProductDesignation ProductDesignationTvg35;
        public ProductDesignation ProductDesignationTvg110;
        public ProductDesignation ProductDesignationTvg220;
        public ProductDesignation ProductDesignationTrg110;
        public ProductDesignation ProductDesignationTrg220;
        public ProductDesignation ProductDesignationPem;
        public ProductDesignation ProductDesignationPPrK;
        public ProductDesignation ProductDesignationPPV;
        public ProductDesignation ProductDesignationTransfBlockVgb35;
        public ProductDesignation ProductDesignationTransfBlockVeb110;
        public ProductDesignation ProductDesignationTransfBlockVeb220;
        public ProductDesignation ProductDesignationZip1;
        public ProductDesignation ProductDesignationZip2;

        private void GenerateProductDesignations()
        {
            ProductDesignationVgb35.Clone(new ProductDesignation { Designation = "ВГБ-УЭТМ-35", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage35kV } });
            ProductDesignationVgt35.Clone(new ProductDesignation { Designation = "ВГТ-УЭТМ-35", Parameters = new List<Parameter> { ParameterBreakerLiveTank, ParameterVoltage35kV } });
            ProductDesignationVgt110.Clone(new ProductDesignation { Designation = "ВГТ-УЭТМ-110", Parameters = new List<Parameter> { ParameterBreakerLiveTank, ParameterVoltage110kV } });

            ProductDesignationVgt2201A1.Clone(new ProductDesignation { Designation = "ВГТ-УЭТМ-1А1-220", Parameters = new List<Parameter> { ParameterBreakerLiveTank, ParameterVoltage220kV, ParameterBreakerLiveTankBreaks1Razriv } });

            ProductDesignationVeb110.Clone(new ProductDesignation { Designation = "ВЭБ-УЭТМ-110", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage110kV } });
            ProductDesignationVeb110II.Clone(new ProductDesignation { Designation = "ВЭБ-УЭТМ-110II*", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage110kV, ParameterDpu2 } });
            ProductDesignationVeb110III.Clone(new ProductDesignation { Designation = "ВЭБ-УЭТМ-110III", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage110kV, ParameterDpu3 } });
            ProductDesignationVeb110IV.Clone(new ProductDesignation { Designation = "ВЭБ-УЭТМ-110IV", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage110kV, ParameterDpu4 } });

            ProductDesignationVeb220.Clone(new ProductDesignation { Designation = "ВЭБ-УЭТМ-220", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage220kV } });

            ProductDesignationZng110.Clone(new ProductDesignation { Designation = "ЗНГ-УЭТМ-110", Parameters = new List<Parameter> { ParameterTransformerVoltage, ParameterVoltage110kV } });
            ProductDesignationZng220.Clone(new ProductDesignation { Designation = "ЗНГ-УЭТМ-220", Parameters = new List<Parameter> { ParameterTransformerVoltage, ParameterVoltage220kV } });

            ProductDesignationTvg35.Clone(new ProductDesignation { Designation = "ТВГ-УЭТМ-35", Parameters = new List<Parameter> { ParameterTransformerCurrent, ParameterTransformerBuiltIn, ParameterVoltage35kV } });
            ProductDesignationTvg110.Clone(new ProductDesignation { Designation = "ТВГ-УЭТМ-110", Parameters = new List<Parameter> { ParameterTransformerCurrent, ParameterTransformerBuiltIn, ParameterVoltage110kV } });
            ProductDesignationTvg220.Clone(new ProductDesignation { Designation = "ТВГ-УЭТМ-220", Parameters = new List<Parameter> { ParameterTransformerCurrent, ParameterTransformerBuiltIn, ParameterVoltage220kV } });

            ProductDesignationTrg110.Clone(new ProductDesignation { Designation = "ТРГ-УЭТМ-110", Parameters = new List<Parameter> { ParameterTransformerCurrent, ParameterTransformerBuiltOut, ParameterVoltage110kV } });
            ProductDesignationTrg220.Clone(new ProductDesignation { Designation = "ТРГ-УЭТМ-220", Parameters = new List<Parameter> { ParameterTransformerCurrent, ParameterTransformerBuiltOut, ParameterVoltage220kV } });

            ProductDesignationPem.Clone(new ProductDesignation { Designation = "ПЭМ", Parameters = new List<Parameter> { ParameterDrivePem } });
            ProductDesignationPPrK.Clone(new ProductDesignation { Designation = "ППрК", Parameters = new List<Parameter> { ParameterDrivePPrK } });
            ProductDesignationPPV.Clone(new ProductDesignation { Designation = "ППВ", Parameters = new List<Parameter> { ParameterDrivePPV } });

            ProductDesignationTransfBlockVgb35.Clone(new ProductDesignation { Designation = "КТТ для ВГБ-35 (3 фазы)", Parameters = new List<Parameter> { ParameterTransformersBlockTargetVgb35 } });
            ProductDesignationTransfBlockVeb110.Clone(new ProductDesignation { Designation = "КТТ для ВЭБ-110 (3 фазы)", Parameters = new List<Parameter> { ParameterTransformersBlockTargetVeb110 } });
            ProductDesignationTransfBlockVeb220.Clone(new ProductDesignation { Designation = "КТТ для ВЭБ-220 (3 фазы)", Parameters = new List<Parameter> { ParameterTransformersBlockTargetVeb220 } });

            ProductDesignationZip1.Clone(new ProductDesignation { Designation = "ЗИП №1", Parameters = new List<Parameter> { ParameterZip1 } });
            ProductDesignationZip2.Clone(new ProductDesignation { Designation = "ЗИП №2", Parameters = new List<Parameter> { ParameterZip2 } });
        }

        #endregion

        #region ProductRelations

        public ProductRelation RequiredChildProductRelationDrivePem;

        public ProductRelation RequiredChildProductRelationDrivePprKVgt35;
        public ProductRelation RequiredChildProductRelationDrivePprKVgt110;
        public ProductRelation RequiredChildProductRelationDrivePprKVeb110;

        public ProductRelation RequiredChildProductRelationDrivePpvVeb110;
        public ProductRelation RequiredChildProductRelationDrivePpv220;
        public ProductRelation RequiredChildProductRelationDrivePpv500;

        public ProductRelation RequiredChildProductRelationBreakerBlock;
        public ProductRelation RequiredChildProductRelationTransfBlockForVeb110;
        public ProductRelation RequiredChildProductRelationTransfBlockForVeb220;
        public ProductRelation RequiredChildProductRelationTransfBlockForVgb35;
        public ProductRelation RequiredChildProductRelationTvg110ForBlock;
        public ProductRelation RequiredChildProductRelationTvg220ForBlock;
        public ProductRelation RequiredChildProductRelationTvg35ForBlock;

        private void GenerateProductRelations()
        {
            RequiredChildProductRelationDrivePem.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterBreaker, ParameterVoltage35kV, ParameterBreakerDeadTank },
                ChildProductParameters = new List<Parameter> { ParameterDrivePem },
                ChildProductsAmount = 1,
                IsUnique = false
            });

            RequiredChildProductRelationDrivePprKVgt35.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterBreakerLiveTank, ParameterVoltage35kV },
                ChildProductParameters = new List<Parameter> { ParameterDrivePPrK },
                ChildProductsAmount = 1,
                IsUnique = false
            });

            RequiredChildProductRelationDrivePprKVgt110.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterBreakerLiveTank, ParameterVoltage110kV },
                ChildProductParameters = new List<Parameter> { ParameterDrivePPrK },
                ChildProductsAmount = 1,
                IsUnique = false
            });

            RequiredChildProductRelationDrivePprKVeb110.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage110kV, ParameterCurrentBreaking40kA },
                ChildProductParameters = new List<Parameter> { ParameterDrivePPrK },
                ChildProductsAmount = 1,
                IsUnique = false
            });

            RequiredChildProductRelationDrivePpvVeb110.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage110kV, ParameterCurrentBreaking50kA },
                ChildProductParameters = new List<Parameter> { ParameterDrivePPV },
                ChildProductsAmount = 1,
                IsUnique = false
            });

            RequiredChildProductRelationDrivePpv220.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterBreaker, ParameterVoltage220kV },
                ChildProductParameters = new List<Parameter> { ParameterDrivePPV },
                ChildProductsAmount = 1,
                IsUnique = false
            });

            RequiredChildProductRelationDrivePpv500.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterBreaker, ParameterVoltage500kV },
                ChildProductParameters = new List<Parameter> { ParameterDrivePPV },
                ChildProductsAmount = 3,
                IsUnique = false
            });

            RequiredChildProductRelationBreakerBlock.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterKtpb },
                ChildProductParameters = new List<Parameter> { ParameterBreaker },
                ChildProductsAmount = 2,
                IsUnique = true
            });

            RequiredChildProductRelationTransfBlockForVeb110.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage110kV, ParameterBreakerPhases3 },
                ChildProductParameters = new List<Parameter> { ParameterTransformersBlockTargetVeb110 },
                ChildProductsAmount = 1,
                IsUnique = false
            });

            RequiredChildProductRelationTransfBlockForVeb220.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage220kV, ParameterBreakerPhases3 },
                ChildProductParameters = new List<Parameter> { ParameterTransformersBlockTargetVeb220 },
                ChildProductsAmount = 1,
                IsUnique = false
            });

            RequiredChildProductRelationTransfBlockForVgb35.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage35kV },
                ChildProductParameters = new List<Parameter> { ParameterTransformersBlockTargetVgb35 },
                ChildProductsAmount = 1,
                IsUnique = false
            });

            RequiredChildProductRelationTvg110ForBlock.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterTransformersBlockTypeCustom, ParameterTransformersBlockTargetVeb110 },
                ChildProductParameters = new List<Parameter> { ParameterTransformerBuiltIn, ParameterVoltage110kV },
                ChildProductsAmount = 6,
                IsUnique = false
            });

            RequiredChildProductRelationTvg220ForBlock.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterTransformersBlockTypeCustom, ParameterTransformersBlockTargetVeb220 },
                ChildProductParameters = new List<Parameter> { ParameterTransformerBuiltIn, ParameterVoltage220kV },
                ChildProductsAmount = 6,
                IsUnique = false
            });

            RequiredChildProductRelationTvg35ForBlock.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterTransformersBlockTypeCustom, ParameterTransformersBlockTargetVgb35 },
                ChildProductParameters = new List<Parameter> { ParameterTransformerBuiltIn, ParameterVoltage35kV },
                ChildProductsAmount = 6,
                IsUnique = false
            });
        }

        #endregion

        #region Products

        public ProductBlock ProductBlockVgb35;
        public ProductBlock ProductBlockVeb110;
        public ProductBlock ProductBlockZng110;
        public ProductBlock ProductBlockDrivePPrK;
        public ProductBlock ProductBlockZip1;
        public ProductBlock ProductBlockSheffMontag;

        public Product ProductVgb35;
        public Product ProductVeb110;
        public Product ProductZng110;
        public Product ProductBreakersDrive;
        public Product ProductZip1;
        public Product ProductSheffMontag;

        private void GenerateProductBlocs()
        {
            ProductBlockVgb35.Clone(new ProductBlock
            {
                //DesignationSpecial = "Блок Выключатель баковый ВГБ-35",
                Parameters = new List<Parameter> { ParameterMainEquipment, ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage35kV },
                Prices = new List<SumOnDate> { new SumOnDate { Sum = 450000, Date = DateTime.Today } },
                StructureCostNumber = "123",
            });

            ProductBlockVeb110.Clone(new ProductBlock
            {
                //DesignationSpecial = "Блок Выключатель баковый ВЭБ-110",
                Parameters = new List<Parameter> { ParameterMainEquipment, ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage110kV },
                Prices = new List<SumOnDate> { new SumOnDate { Sum = 2000000, Date = DateTime.Today } },
                StructureCostNumber = "321",
            });

            ProductBlockZng110.Clone(new ProductBlock
            {
                //DesignationSpecial = "Блок Трансформатор напряжения ЗНГ-110",
                Parameters = new List<Parameter> { ParameterMainEquipment, ParameterTransformer, ParameterTransformerVoltage, ParameterVoltage110kV },
                Prices = new List<SumOnDate> { new SumOnDate { Sum = 250000, Date = DateTime.Today.AddDays(-95) } },
                StructureCostNumber = "456"
            });

            ProductBlockDrivePPrK.Clone(new ProductBlock
            {
                DesignationSpecial = "ППрК",
                Parameters = new List<Parameter> { ParameterProductParts, ParameterPartDrive, ParameterDrivePPrK, ParameterDrivesVoltage220V },
                Prices = new List<SumOnDate> { new SumOnDate { Sum = 200000, Date = DateTime.Today } },
                StructureCostNumber = "654"
            });

            ProductBlockZip1.Clone(new ProductBlock
            {
                DesignationSpecial = "Блок Групповой комплект ЗИП №1",
                Parameters = new List<Parameter> { ParameterDependentEquipment, ParameterZip1 },
                Prices = new List<SumOnDate> { new SumOnDate { Sum = 2500, Date = DateTime.Today } },
                StructureCostNumber = "789"
            });

            ProductBlockSheffMontag.Clone(new ProductBlock
            {
                DesignationSpecial = "Шеф-монтаж",
                Parameters = new List<Parameter> { ParameterService, ParameterSheffMontag },
                Prices = new List<SumOnDate> { new SumOnDate { Sum = 60000, Date = DateTime.Today } },
                FixedCosts = new List<SumOnDate> { new SumOnDate { Sum = 120000, Date = DateTime.Today } },
                StructureCostNumber = "---"
            });
        }

        private void GenerateProducts()
        {
            ProductVgb35.Clone(new Product
            {
                //DesignationSpecial = "ВГБ-35",
                ProductBlock = ProductBlockVgb35,
                DependentProducts = new List<ProductDependent> { new ProductDependent { Product = ProductBreakersDrive } }
            });

            ProductVeb110.Clone(new Product
            {
                //DesignationSpecial = "ВЭБ-110",
                ProductBlock = ProductBlockVeb110,
                DependentProducts = new List<ProductDependent> { new ProductDependent { Product = ProductBreakersDrive } }
            });

            ProductZng110.Clone(new Product
            {
                //DesignationSpecial = "Трансформатор напряжения ЗНГ-110",
                ProductBlock = ProductBlockZng110
            });

            ProductBreakersDrive.Clone(new Product
            {
                //DesignationSpecial = "Привод выключателя",
                ProductBlock = ProductBlockDrivePPrK
            });

            ProductZip1.Clone(new Product
            {
                //DesignationSpecial = "ЗиП №1",
                ProductBlock = ProductBlockZip1
            });

            ProductSheffMontag.Clone(new Product
            {
                ProductBlock = ProductBlockSheffMontag
            });
        }


        #endregion

        #region IsService

        public ProductBlockIsService ProductBlockIsService;

        private void GenerateProductBlockIsService()
        {
            ProductBlockIsService.Clone(new ProductBlockIsService { Parameters = new List<Parameter>() { ParameterService } });
        }       

        #endregion

    }
}
