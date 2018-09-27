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
        public ParameterGroup ParameterGroupVoltage;
        public ParameterGroup ParameterGroupDrivesVoltage;
        public ParameterGroup ParameterGroupIsolation;
        public ParameterGroup ParameterGroupIsolationMaterial;
        public ParameterGroup ParameterGroupAccuracy;
        public ParameterGroup ParameterGroupCurrent;
        public ParameterGroup ParameterGroupNewProductDesignation;
        public ParameterGroup ParameterGroupDrives;
        public ParameterGroup ParameterGroupClimat;
        public ParameterGroup ParameterGroupPartType;
        public ParameterGroup ParameterGroupTransformersBlockStandartNumber;
        public ParameterGroup ParameterGroupTransformersBlockType;
        public ParameterGroup ParameterGroupTransformersBlockTarget;

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
            ParameterGroupNewProductDesignation.Clone(new ParameterGroup { Name = "Обозначение" });
            ParameterGroupDrives.Clone(new ParameterGroup { Name = "Приводы" });
            ParameterGroupClimat.Clone(new ParameterGroup { Name = "Климатическое исполнение" });
            ParameterGroupPartType.Clone(new ParameterGroup { Name = "Тип составной части" });
            ParameterGroupTransformersBlockType.Clone(new ParameterGroup { Name = "Тип комплекта ТТ" });
            ParameterGroupTransformersBlockTarget.Clone(new ParameterGroup { Name = "Назначение комплекта ТТ" });
            ParameterGroupTransformersBlockStandartNumber.Clone(new ParameterGroup { Name = "Номер стандартного комплекта ТТ" });
        }

        #endregion

        #region Parameter

        public Parameter ParameterNewProduct;
        public Parameter ParameterMainEquipment;
        public Parameter ParameterDependentEquipment;
        public Parameter ParameterService;

        public Parameter ParameterZip1;
        public Parameter ParameterZip2;

        public Parameter ParameterBreaker;
        public Parameter ParameterTransformer;
        public Parameter ParameterDisconnector;
        public Parameter ParameterEarthingSwitch;
        public Parameter ParameterProductParts;
        public Parameter ParameterKtpb;

        public Parameter ParameterPartDrive;
        public Parameter ParameterPartTransformer;
        public Parameter ParameterPartTransformersBlock;

        public Parameter ParameterDrivePPrK;
        public Parameter ParameterDrivePPV;
        public Parameter ParameterDriveDisconnector;

        public Parameter ParameterBreakerDeadTank;
        public Parameter ParameterBreakerLiveTank;
        public Parameter ParameterTransformerCurrent;
        public Parameter ParameterTransformerVoltage;

        public Parameter ParameterVoltage35kV;
        public Parameter ParameterVoltage110kV;
        public Parameter ParameterVoltage220kV;
        public Parameter ParameterVoltage500kV;

        public Parameter ParameterVoltage110V;
        public Parameter ParameterVoltage220V;

        public Parameter ParameterTransformerBuiltOut;
        public Parameter ParameterTransformerBuiltIn;

        public Parameter ParameterDpu2;
        public Parameter ParameterDpu3;
        public Parameter ParameterDpu4;

        public Parameter ParameterFarfor;
        public Parameter ParameterPolimer;

        public Parameter ParameterAccuracy05P;
        public Parameter ParameterAccuracy10P;

        public Parameter ParameterCurrent2500;
        public Parameter ParameterCurrent3150;
        public Parameter ParameterCurrent4000;

        public Parameter ParameterClimatU1z;
        public Parameter ParameterClimatUHL1z;
        public Parameter ParameterClimatUHL1;
        public Parameter ParameterClimatU1;
        public Parameter ParameterClimatHL1z;

        public Parameter ParameterTransformersBlockStandartVeb110Num1;
        public Parameter ParameterTransformersBlockStandartVeb110Num2;

        public Parameter ParameterTransformersBlockTypeStandart;
        public Parameter ParameterTransformersBlockTypeCustom;

        public Parameter ParameterTransformersBlockTargetVeb110;
        public Parameter ParameterTransformersBlockTargetVeb220;

        private void GenerateParameters()
        {
            ParameterNewProduct.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Оборудование новое" });
            ParameterMainEquipment.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Оборудование главное" });
            ParameterDependentEquipment.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Оборудование дополнительное" });
            ParameterService.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Услуга" });
            ParameterProductParts.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Составные части оборудования" });

            ParameterBreaker.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "Выключатель" });
            ParameterTransformer.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "Трансформатор" });
            ParameterDisconnector.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "Разъединитель" });
            ParameterEarthingSwitch.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "Заземлитель" });
            ParameterKtpb.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "КТПБ" });

            ParameterPartDrive.Clone(new Parameter { ParameterGroup = ParameterGroupPartType, Value = "Привод" });
            ParameterPartTransformer.Clone(new Parameter { ParameterGroup = ParameterGroupPartType, Value = "Трансформатор" });
            ParameterPartTransformersBlock.Clone(new Parameter { ParameterGroup = ParameterGroupPartType, Value = "Блок трансформаторов" });

            ParameterDrivePPrK.Clone(new Parameter { ParameterGroup = ParameterGroupDrives, Value = "Привод ППрК" });
            ParameterDrivePPV.Clone(new Parameter { ParameterGroup = ParameterGroupDrives, Value = "Привод ППВ" });
            ParameterDriveDisconnector.Clone(new Parameter { ParameterGroup = ParameterGroupDrives, Value = "Привод разъединителя/заземлителя" });

            ParameterZip1.Clone(new Parameter { ParameterGroup = ParameterGroupZip, Value = "Групповой комплект ЗИП №1 (газотехнология)" });
            ParameterZip2.Clone(new Parameter { ParameterGroup = ParameterGroupZip, Value = "Групповой комплект ЗИП №2 (элегаз)" });

            ParameterBreakerDeadTank.Clone(new Parameter { ParameterGroup = ParameterGroupBreakerType, Value = "Баковый" });
            ParameterBreakerLiveTank.Clone(new Parameter { ParameterGroup = ParameterGroupBreakerType, Value = "Колонковый" });

            ParameterTransformerCurrent.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorType, Value = "Тока" });
            ParameterTransformerVoltage.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorType, Value = "Напряжения" });

            ParameterVoltage35kV.Clone(new Parameter { ParameterGroup = ParameterGroupVoltage, Value = "35 кВ" });
            ParameterVoltage110kV.Clone(new Parameter { ParameterGroup = ParameterGroupVoltage, Value = "110 кВ" });
            ParameterVoltage220kV.Clone(new Parameter { ParameterGroup = ParameterGroupVoltage, Value = "220 кВ" });
            ParameterVoltage500kV.Clone(new Parameter { ParameterGroup = ParameterGroupVoltage, Value = "500 кВ" });

            ParameterVoltage110V.Clone(new Parameter { ParameterGroup = ParameterGroupDrivesVoltage, Value = "110 В" });
            ParameterVoltage220V.Clone(new Parameter { ParameterGroup = ParameterGroupDrivesVoltage, Value = "220 В" });

            ParameterTransformerBuiltOut.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorCurrentType, Value = "Отдельностоящий" });
            ParameterTransformerBuiltIn.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorCurrentType, Value = "Встроенный" });

            ParameterFarfor.Clone(new Parameter { ParameterGroup = ParameterGroupIsolationMaterial, Value = "Фарфор" });
            ParameterPolimer.Clone(new Parameter { ParameterGroup = ParameterGroupIsolationMaterial, Value = "Полимер" });

            ParameterDpu2.Clone(new Parameter { ParameterGroup = ParameterGroupIsolation, Value = "II*" });
            ParameterDpu3.Clone(new Parameter { ParameterGroup = ParameterGroupIsolation, Value = "III" });
            ParameterDpu4.Clone(new Parameter { ParameterGroup = ParameterGroupIsolation, Value = "IV" });

            ParameterAccuracy05P.Clone(new Parameter { ParameterGroup = ParameterGroupAccuracy, Value = "5P" });
            ParameterAccuracy10P.Clone(new Parameter { ParameterGroup = ParameterGroupAccuracy, Value = "10P" });

            ParameterCurrent2500.Clone(new Parameter { ParameterGroup = ParameterGroupCurrent, Value = "2500 А" });
            ParameterCurrent3150.Clone(new Parameter { ParameterGroup = ParameterGroupCurrent, Value = "3150 А" });
            ParameterCurrent4000.Clone(new Parameter { ParameterGroup = ParameterGroupCurrent, Value = "4000 А" });

            ParameterClimatU1z.Clone(new Parameter { ParameterGroup = ParameterGroupClimat, Value = "У1* (-40)" });
            ParameterClimatUHL1z.Clone(new Parameter { ParameterGroup = ParameterGroupClimat, Value = "УХЛ1* (-55)" });
            ParameterClimatUHL1.Clone(new Parameter { ParameterGroup = ParameterGroupClimat, Value = "УХЛ1 (-60)" });
            ParameterClimatU1.Clone(new Parameter { ParameterGroup = ParameterGroupClimat, Value = "У1 (-45)" });
            ParameterClimatHL1z.Clone(new Parameter { ParameterGroup = ParameterGroupClimat, Value = "ХЛ1* (-55)" });

            ParameterTransformersBlockTypeStandart.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockType, Value = "Стандартный" });
            ParameterTransformersBlockTypeCustom.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockType, Value = "По заказу" });

            ParameterTransformersBlockTargetVeb110.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockTarget, Value = "Для ВЭБ-110 (3 фазы)" });
            ParameterTransformersBlockTargetVeb220.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockTarget, Value = "Для ВЭБ-220 (3 фазы)" });

            ParameterTransformersBlockStandartVeb110Num1.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockStandartNumber, Value = "602-231 (300-200-150-100/5)" });
            ParameterTransformersBlockStandartVeb110Num2.Clone(new Parameter { ParameterGroup = ParameterGroupTransformersBlockStandartNumber, Value = "602-112 (600-400-300-200/5)" });
        }

        private void GenerateRelations()
        {
            ParameterBreaker.AddRequiredPreviousParameters(ParameterMainEquipment);
            ParameterDisconnector.AddRequiredPreviousParameters(ParameterMainEquipment);
            ParameterEarthingSwitch.AddRequiredPreviousParameters(ParameterMainEquipment);
            ParameterKtpb.AddRequiredPreviousParameters(ParameterMainEquipment);
            ParameterTransformer.AddRequiredPreviousParameters(ParameterMainEquipment)
                                .AddRequiredPreviousParameters(ParameterPartTransformer);

            ParameterPartDrive.AddRequiredPreviousParameters(ParameterProductParts);
            ParameterPartTransformer.AddRequiredPreviousParameters(ParameterProductParts);
            ParameterPartTransformersBlock.AddRequiredPreviousParameters(ParameterProductParts);


            ParameterDrivePPrK.AddRequiredPreviousParameters(ParameterPartDrive);
            ParameterDrivePPV.AddRequiredPreviousParameters(ParameterPartDrive);
            ParameterDriveDisconnector.AddRequiredPreviousParameters(ParameterPartDrive);

            ParameterBreakerDeadTank.AddRequiredPreviousParameters(ParameterBreaker);
            ParameterBreakerLiveTank.AddRequiredPreviousParameters(ParameterBreaker);

            ParameterZip1.AddRequiredPreviousParameters(ParameterDependentEquipment);
            ParameterZip2.AddRequiredPreviousParameters(ParameterDependentEquipment);

            ParameterTransformerCurrent.AddRequiredPreviousParameters(ParameterTransformer);
            ParameterTransformerVoltage.AddRequiredPreviousParameters(ParameterTransformer, ParameterMainEquipment);

            ParameterAccuracy05P.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);
            ParameterAccuracy10P.AddRequiredPreviousParameters(ParameterTransformerBuiltIn);

            ParameterCurrent2500.AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage110kV)
                                .AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage220kV);
            ParameterCurrent3150.AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage110kV)
                                .AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage220kV);
            ParameterCurrent4000.AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage500kV);

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

            ParameterVoltage110V.AddRequiredPreviousParameters(ParameterDrivePPrK);
            ParameterVoltage220V.AddRequiredPreviousParameters(ParameterDrivePPrK);

            ParameterTransformerBuiltOut.AddRequiredPreviousParameters(ParameterTransformerCurrent, ParameterMainEquipment);

            ParameterTransformerBuiltIn.AddRequiredPreviousParameters(ParameterTransformerCurrent, ParameterPartTransformer);

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

            ParameterDpu2.AddRequiredPreviousParameters(ParameterFarfor);
            ParameterDpu3.AddRequiredPreviousParameters(ParameterFarfor);
            ParameterDpu4.AddRequiredPreviousParameters(ParameterFarfor)
                         .AddRequiredPreviousParameters(ParameterPolimer);


            ParameterClimatU1z.AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage110kV);
            ParameterClimatUHL1z.AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage110kV);
            ParameterClimatUHL1.AddRequiredPreviousParameters(ParameterBreakerDeadTank, ParameterVoltage110kV);
            ParameterClimatU1.AddRequiredPreviousParameters(ParameterBreakerLiveTank, ParameterVoltage110kV);
            ParameterClimatHL1z.AddRequiredPreviousParameters(ParameterBreakerLiveTank, ParameterVoltage110kV);

            ParameterTransformersBlockTypeStandart.AddRequiredPreviousParameters(ParameterPartTransformersBlock);
            ParameterTransformersBlockTypeCustom.AddRequiredPreviousParameters(ParameterPartTransformersBlock);

            ParameterTransformersBlockTargetVeb110.AddRequiredPreviousParameters(ParameterPartTransformersBlock);
            ParameterTransformersBlockTargetVeb220.AddRequiredPreviousParameters(ParameterPartTransformersBlock);

            ParameterTransformersBlockStandartVeb110Num1.AddRequiredPreviousParameters(ParameterTransformersBlockTargetVeb110);
            ParameterTransformersBlockStandartVeb110Num2.AddRequiredPreviousParameters(ParameterTransformersBlockTargetVeb110);
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
        public ProductDesignation ProductDesignationVeb110;
        public ProductDesignation ProductDesignationVeb110II;
        public ProductDesignation ProductDesignationZng110;
        public ProductDesignation ProductDesignationVeb220;
        public ProductDesignation ProductDesignationZng220;
        public ProductDesignation ProductDesignationTvg110;
        public ProductDesignation ProductDesignationTvg220;
        public ProductDesignation ProductDesignationTrg110;
        public ProductDesignation ProductDesignationTrg220;
        public ProductDesignation ProductDesignationPPrK;

        private void GenerateProductDesignations()
        {
            ProductDesignationVgb35.Clone(new ProductDesignation { Designation = "ВГБ-УЭТМ-35", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage35kV } });
            ProductDesignationVeb110.Clone(new ProductDesignation { Designation = "ВЭБ-УЭТМ-110", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage110kV } });
            ProductDesignationVeb110II.Clone(new ProductDesignation { Designation = "ВЭБ-УЭТМ-110II*", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage110kV, ParameterDpu2 } });
            ProductDesignationVeb220.Clone(new ProductDesignation { Designation = "ВЭБ-УЭТМ-220", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage220kV } });
            ProductDesignationZng110.Clone(new ProductDesignation { Designation = "ЗНГ-УЭТМ-110", Parameters = new List<Parameter> { ParameterTransformerVoltage, ParameterVoltage110kV } });
            ProductDesignationZng220.Clone(new ProductDesignation { Designation = "ЗНГ-УЭТМ-220", Parameters = new List<Parameter> { ParameterTransformerVoltage, ParameterVoltage220kV } });
            ProductDesignationTvg110.Clone(new ProductDesignation { Designation = "ТВГ-УЭТМ-110", Parameters = new List<Parameter> { ParameterTransformerCurrent, ParameterTransformerBuiltIn, ParameterVoltage110kV } });
            ProductDesignationTvg220.Clone(new ProductDesignation { Designation = "ТВГ-УЭТМ-220", Parameters = new List<Parameter> { ParameterTransformerCurrent, ParameterTransformerBuiltIn, ParameterVoltage220kV } });
            ProductDesignationTrg110.Clone(new ProductDesignation { Designation = "ТРГ-УЭТМ-110", Parameters = new List<Parameter> { ParameterTransformerCurrent, ParameterTransformerBuiltOut, ParameterVoltage110kV } });
            ProductDesignationTrg220.Clone(new ProductDesignation { Designation = "ТРГ-УЭТМ-220", Parameters = new List<Parameter> { ParameterTransformerCurrent, ParameterTransformerBuiltOut, ParameterVoltage220kV } });
            ProductDesignationPPrK.Clone(new ProductDesignation { Designation = "Привод ППрК", Parameters = new List<Parameter> { ParameterDrivePPrK } });
        }

        #endregion

        #region ProductRelations

        public ProductRelation RequiredChildProductRelationDrivePPrK;
        public ProductRelation RequiredChildProductRelationDrivePPV220;
        public ProductRelation RequiredChildProductRelationDrivePPV500;
        public ProductRelation RequiredChildProductRelationBreakerBlock;
        public ProductRelation RequiredChildProductRelationTransfBlockForVeb110;
        public ProductRelation RequiredChildProductRelationTvg110ForBlock;

        private void GenerateProductRelations()
        {
            RequiredChildProductRelationDrivePPrK.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterBreaker, ParameterVoltage110kV },
                ChildProductParameters = new List<Parameter> { ParameterDrivePPrK },
                ChildProductsAmount = 1,
                IsUnique = false
            });

            RequiredChildProductRelationDrivePPV220.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { ParameterBreaker, ParameterVoltage220kV },
                ChildProductParameters = new List<Parameter> { ParameterDrivePPV },
                ChildProductsAmount = 1,
                IsUnique = false
            });

            RequiredChildProductRelationDrivePPV500.Clone(new ProductRelation
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
                ParentProductParameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage110kV },
                ChildProductParameters = new List<Parameter> { ParameterTransformersBlockTargetVeb110 },
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
        }

        #endregion

        #region Products

        public ProductBlock ProductBlockVgb35;
        public ProductBlock ProductBlockVeb110;
        public ProductBlock ProductBlockZng110;
        public ProductBlock ProductBlockDrivePPrK;
        public ProductBlock ProductBlockZip1;

        public Product ProductVgb35;
        public Product ProductVeb110;
        public Product ProductZng110;
        public Product ProductBreakersDrive;
        public Product ProductZip1;

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
                Parameters = new List<Parameter> { ParameterProductParts, ParameterPartDrive, ParameterDrivePPrK, ParameterVoltage220V },
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
