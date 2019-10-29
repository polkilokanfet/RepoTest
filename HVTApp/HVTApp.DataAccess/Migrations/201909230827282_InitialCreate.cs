namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityField",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ActivityFieldEnum = c.Int(nullable: false),
                        MarketField_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MarketField", t => t.MarketField_Id)
                .Index(t => t.MarketField_Id);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(nullable: false, maxLength: 100),
                        Locality_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locality", t => t.Locality_Id)
                .Index(t => t.Locality_Id);
            
            CreateTable(
                "dbo.Locality",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsCountryCapital = c.Boolean(nullable: false),
                        IsDistrictCapital = c.Boolean(nullable: false),
                        IsRegionCapital = c.Boolean(nullable: false),
                        DistanceToEkb = c.Double(),
                        LocalityType_Id = c.Guid(nullable: false),
                        Region_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LocalityType", t => t.LocalityType_Id)
                .ForeignKey("dbo.Region", t => t.Region_Id)
                .Index(t => t.LocalityType_Id)
                .Index(t => t.Region_Id);
            
            CreateTable(
                "dbo.LocalityType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 50),
                        ShortName = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Region",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        District_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.District", t => t.District_Id)
                .Index(t => t.District_Id);
            
            CreateTable(
                "dbo.District",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Country_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        CountryUnion_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CountryUnion", t => t.CountryUnion_Id)
                .Index(t => t.CountryUnion_Id);
            
            CreateTable(
                "dbo.BankDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BankName = c.String(nullable: false, maxLength: 50),
                        BankIdentificationCode = c.String(nullable: false, maxLength: 10),
                        CorrespondentAccount = c.String(nullable: false, maxLength: 50),
                        CheckingAccount = c.String(nullable: false, maxLength: 50),
                        Company_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Company_Id, cascadeDelete: true)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.BankGuarantee",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Percent = c.Double(nullable: false),
                        Days = c.Int(nullable: false),
                        BankGuaranteeType_Id = c.Guid(nullable: false),
                        SalesUnit_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankGuaranteeType", t => t.BankGuaranteeType_Id)
                .ForeignKey("dbo.SalesUnit", t => t.SalesUnit_Id, cascadeDelete: true)
                .Index(t => t.BankGuaranteeType_Id)
                .Index(t => t.SalesUnit_Id);
            
            CreateTable(
                "dbo.BankGuaranteeType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 100),
                        ShortName = c.String(nullable: false, maxLength: 100),
                        Inn = c.String(maxLength: 10),
                        Kpp = c.String(maxLength: 10),
                        AddressLegal_Id = c.Guid(),
                        AddressPost_Id = c.Guid(),
                        Form_Id = c.Guid(nullable: false),
                        ParentCompany_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressLegal_Id)
                .ForeignKey("dbo.Address", t => t.AddressPost_Id)
                .ForeignKey("dbo.CompanyForm", t => t.Form_Id)
                .ForeignKey("dbo.Company", t => t.ParentCompany_Id)
                .Index(t => t.AddressLegal_Id)
                .Index(t => t.AddressPost_Id)
                .Index(t => t.Form_Id)
                .Index(t => t.ParentCompany_Id);
            
            CreateTable(
                "dbo.CompanyForm",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 100),
                        ShortName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(nullable: false, maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        Contragent_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Contragent_Id)
                .Index(t => t.Contragent_Id);
            
            CreateTable(
                "dbo.CountryUnion",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CreateNewProductTask",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Designation = c.String(nullable: false, maxLength: 50),
                        StructureCostNumber = c.String(nullable: false, maxLength: 10),
                        Comment = c.String(maxLength: 150),
                        Product_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DesignationSpecial = c.String(maxLength: 50),
                        ProductBlock_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductBlock", t => t.ProductBlock_Id)
                .Index(t => t.ProductBlock_Id);
            
            CreateTable(
                "dbo.ProductDependent",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MainProductId = c.Guid(nullable: false),
                        Amount = c.Int(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .ForeignKey("dbo.Product", t => t.MainProductId)
                .Index(t => t.MainProductId)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductBlock",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DesignationSpecial = c.String(maxLength: 50),
                        StructureCostNumber = c.String(maxLength: 10),
                        Design = c.String(maxLength: 25),
                        Weight = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SumOnDate",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Sum = c.Double(nullable: false),
                        ProductBlock_Id = c.Guid(),
                        ProductBlock_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductBlock", t => t.ProductBlock_Id)
                .ForeignKey("dbo.ProductBlock", t => t.ProductBlock_Id1)
                .Index(t => t.ProductBlock_Id)
                .Index(t => t.ProductBlock_Id1);
            
            CreateTable(
                "dbo.Parameter",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(nullable: false, maxLength: 150),
                        Rang = c.Int(nullable: false),
                        Comment = c.String(maxLength: 75),
                        ParameterGroup_Id = c.Guid(nullable: false),
                        StandartMarginalIncome_Id = c.Guid(),
                        StandartProductionTerm_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParameterGroup", t => t.ParameterGroup_Id)
                .ForeignKey("dbo.StandartMarginalIncome", t => t.StandartMarginalIncome_Id)
                .ForeignKey("dbo.StandartProductionTerm", t => t.StandartProductionTerm_Id)
                .Index(t => t.ParameterGroup_Id)
                .Index(t => t.StandartMarginalIncome_Id)
                .Index(t => t.StandartProductionTerm_Id);
            
            CreateTable(
                "dbo.ParameterGroup",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Comment = c.String(maxLength: 75),
                        Measure_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Measure", t => t.Measure_Id)
                .Index(t => t.Measure_Id);
            
            CreateTable(
                "dbo.Measure",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 25),
                        ShortName = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParameterRelation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParameterId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parameter", t => t.ParameterId)
                .Index(t => t.ParameterId);
            
            CreateTable(
                "dbo.CurrencyExchangeRate",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        FirstCurrency = c.Int(nullable: false),
                        SecondCurrency = c.Int(nullable: false),
                        ExchangeRate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        SenderId = c.Guid(nullable: false),
                        RecipientId = c.Guid(nullable: false),
                        Comment = c.String(maxLength: 100),
                        ValidityDate = c.DateTime(),
                        Vat = c.Double(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Author_Id = c.Guid(),
                        Number_Number = c.Int(nullable: false),
                        RequestDocument_Id = c.Guid(),
                        Project_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.Author_Id)
                .ForeignKey("dbo.DocumentNumber", t => t.Number_Number)
                .ForeignKey("dbo.Employee", t => t.RecipientId)
                .ForeignKey("dbo.Document", t => t.RequestDocument_Id)
                .ForeignKey("dbo.Employee", t => t.SenderId)
                .ForeignKey("dbo.Project", t => t.Project_Id, cascadeDelete: true)
                .Index(t => t.SenderId)
                .Index(t => t.RecipientId)
                .Index(t => t.Author_Id)
                .Index(t => t.Number_Number)
                .Index(t => t.RequestDocument_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PersonalNumber = c.String(maxLength: 10),
                        PhoneNumber = c.String(maxLength: 20),
                        Email = c.String(maxLength: 50),
                        Company_Id = c.Guid(nullable: false),
                        Person_Id = c.Guid(nullable: false),
                        Position_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .ForeignKey("dbo.Person", t => t.Person_Id, cascadeDelete: true)
                .ForeignKey("dbo.EmployeesPosition", t => t.Position_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.Person_Id)
                .Index(t => t.Position_Id);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Surname = c.String(nullable: false, maxLength: 30),
                        Name = c.String(nullable: false, maxLength: 30),
                        Patronymic = c.String(maxLength: 30),
                        IsMan = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeesPosition",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DocumentNumber",
                c => new
                    {
                        Number = c.Int(nullable: false, identity: true),
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Number);
            
            CreateTable(
                "dbo.DocumentsRegistrationDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Number = c.String(nullable: false, maxLength: 15),
                        Document_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Document", t => t.Document_Id, cascadeDelete: true)
                .Index(t => t.Document_Id);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        InWork = c.Boolean(nullable: false),
                        ForReport = c.Boolean(nullable: false),
                        Manager_Id = c.Guid(nullable: false),
                        ProjectType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Manager_Id)
                .ForeignKey("dbo.ProjectType", t => t.ProjectType_Id)
                .Index(t => t.Manager_Id)
                .Index(t => t.ProjectType_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Login = c.String(nullable: false, maxLength: 20),
                        Password = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 15),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Note",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Text = c.String(nullable: false, maxLength: 150),
                        IsImportant = c.Boolean(nullable: false),
                        Project_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.Project_Id, cascadeDelete: true)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.ProjectType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Facility",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Address_Id = c.Guid(),
                        OwnerCompany_Id = c.Guid(nullable: false),
                        Type_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.Address_Id)
                .ForeignKey("dbo.Company", t => t.OwnerCompany_Id)
                .ForeignKey("dbo.FacilityType", t => t.Type_Id)
                .Index(t => t.Address_Id)
                .Index(t => t.OwnerCompany_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.FacilityType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 50),
                        ShortName = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FakeData",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Cost = c.Double(),
                        RealizationDate = c.DateTime(),
                        OrderInTakeDate = c.DateTime(),
                        PaymentConditionSet_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentConditionSet", t => t.PaymentConditionSet_Id)
                .ForeignKey("dbo.SalesUnit", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.PaymentConditionSet_Id);
            
            CreateTable(
                "dbo.PaymentConditionSet",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentCondition",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Part = c.Double(nullable: false),
                        DaysToPoint = c.Int(nullable: false),
                        PaymentConditionPoint_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentConditionPoint", t => t.PaymentConditionPoint_Id)
                .Index(t => t.PaymentConditionPoint_Id);
            
            CreateTable(
                "dbo.PaymentConditionPoint",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        PaymentConditionPointEnum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GlobalProperties",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ActualPriceTerm = c.Int(nullable: false),
                        StandartTermFromStartToEndProduction = c.Int(nullable: false),
                        StandartTermFromPickToEndProduction = c.Int(nullable: false),
                        Vat = c.Double(nullable: false),
                        HvtProducersActivityField_Id = c.Guid(nullable: false),
                        NewProductParameter_Id = c.Guid(nullable: false),
                        NewProductParameterGroup_Id = c.Guid(nullable: false),
                        OurCompany_Id = c.Guid(nullable: false),
                        PaymentConditionSet_Id = c.Guid(nullable: false),
                        SenderOfferEmployee_Id = c.Guid(nullable: false),
                        ServiceParameter_Id = c.Guid(nullable: false),
                        StandartPaymentsConditionSet_Id = c.Guid(nullable: false),
                        SupervisionParameter_Id = c.Guid(nullable: false),
                        VoltageGroup_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActivityField", t => t.HvtProducersActivityField_Id)
                .ForeignKey("dbo.Parameter", t => t.NewProductParameter_Id)
                .ForeignKey("dbo.ParameterGroup", t => t.NewProductParameterGroup_Id)
                .ForeignKey("dbo.Company", t => t.OurCompany_Id, cascadeDelete: true)
                .ForeignKey("dbo.PaymentConditionSet", t => t.PaymentConditionSet_Id)
                .ForeignKey("dbo.Employee", t => t.SenderOfferEmployee_Id)
                .ForeignKey("dbo.Parameter", t => t.ServiceParameter_Id)
                .ForeignKey("dbo.PaymentConditionSet", t => t.StandartPaymentsConditionSet_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.SupervisionParameter_Id)
                .ForeignKey("dbo.ParameterGroup", t => t.VoltageGroup_Id)
                .Index(t => t.HvtProducersActivityField_Id)
                .Index(t => t.NewProductParameter_Id)
                .Index(t => t.NewProductParameterGroup_Id)
                .Index(t => t.OurCompany_Id)
                .Index(t => t.PaymentConditionSet_Id)
                .Index(t => t.SenderOfferEmployee_Id)
                .Index(t => t.ServiceParameter_Id)
                .Index(t => t.StandartPaymentsConditionSet_Id)
                .Index(t => t.SupervisionParameter_Id)
                .Index(t => t.VoltageGroup_Id);
            
            CreateTable(
                "dbo.LosingReason",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MarketField",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OfferUnit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Cost = c.Double(nullable: false),
                        CostDelivery = c.Double(),
                        CostDeliveryIncluded = c.Boolean(nullable: false),
                        ProductionTerm = c.Int(nullable: false),
                        Facility_Id = c.Guid(nullable: false),
                        Offer_Id = c.Guid(nullable: false),
                        PaymentConditionSet_Id = c.Guid(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Facility", t => t.Facility_Id)
                .ForeignKey("dbo.Document", t => t.Offer_Id, cascadeDelete: true)
                .ForeignKey("dbo.PaymentConditionSet", t => t.PaymentConditionSet_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Facility_Id)
                .Index(t => t.Offer_Id)
                .Index(t => t.PaymentConditionSet_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductIncluded",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Int(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(nullable: false, maxLength: 10),
                        DateOpen = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentActual",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Sum = c.Double(nullable: false),
                        Comment = c.String(maxLength: 50),
                        PaymentDocument_Id = c.Guid(nullable: false),
                        SalesUnit_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentDocument", t => t.PaymentDocument_Id, cascadeDelete: true)
                .ForeignKey("dbo.SalesUnit", t => t.SalesUnit_Id, cascadeDelete: true)
                .Index(t => t.PaymentDocument_Id)
                .Index(t => t.SalesUnit_Id);
            
            CreateTable(
                "dbo.PaymentDocument",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(maxLength: 25),
                        Vat = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentPlanned",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Part = c.Double(nullable: false),
                        Comment = c.String(maxLength: 50),
                        Condition_Id = c.Guid(nullable: false),
                        SalesUnit_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentCondition", t => t.Condition_Id)
                .ForeignKey("dbo.SalesUnit", t => t.SalesUnit_Id, cascadeDelete: true)
                .Index(t => t.Condition_Id)
                .Index(t => t.SalesUnit_Id);
            
            CreateTable(
                "dbo.Penalty",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PercentPerDay = c.Double(nullable: false),
                        PercentLimit = c.Double(nullable: false),
                        PenaltyPaid = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SalesUnit", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ProductDesignation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Designation = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductRelation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 75),
                        ChildProductsAmount = c.Int(nullable: false),
                        IsUnique = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 75),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTypeDesignation", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ProductTypeDesignation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SalesUnit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Cost = c.Double(nullable: false),
                        Price = c.Double(),
                        ProductionTerm = c.Int(),
                        DeliveryDateExpected = c.DateTime(nullable: false),
                        RealizationDate = c.DateTime(),
                        TceRequest = c.String(maxLength: 20),
                        OrderPosition = c.String(maxLength: 4),
                        SerialNumber = c.String(maxLength: 10),
                        AssembleTerm = c.Int(),
                        SignalToStartProduction = c.DateTime(),
                        SignalToStartProductionDone = c.DateTime(),
                        StartProductionDate = c.DateTime(),
                        PickingDate = c.DateTime(),
                        EndProductionPlanDate = c.DateTime(),
                        EndProductionDate = c.DateTime(),
                        CostDelivery = c.Double(),
                        CostDeliveryIncluded = c.Boolean(nullable: false),
                        ExpectedDeliveryPeriod = c.Int(),
                        ShipmentDate = c.DateTime(),
                        ShipmentPlanDate = c.DateTime(),
                        DeliveryDate = c.DateTime(),
                        AddressDelivery_Id = c.Guid(),
                        Facility_Id = c.Guid(nullable: false),
                        Order_Id = c.Guid(),
                        PaymentConditionSet_Id = c.Guid(nullable: false),
                        Producer_Id = c.Guid(),
                        Product_Id = c.Guid(nullable: false),
                        Project_Id = c.Guid(nullable: false),
                        Specification_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressDelivery_Id)
                .ForeignKey("dbo.Facility", t => t.Facility_Id)
                .ForeignKey("dbo.Order", t => t.Order_Id)
                .ForeignKey("dbo.PaymentConditionSet", t => t.PaymentConditionSet_Id)
                .ForeignKey("dbo.Company", t => t.Producer_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .ForeignKey("dbo.Project", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.Specification", t => t.Specification_Id)
                .Index(t => t.AddressDelivery_Id)
                .Index(t => t.Facility_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.PaymentConditionSet_Id)
                .Index(t => t.Producer_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Project_Id)
                .Index(t => t.Specification_Id);
            
            CreateTable(
                "dbo.Specification",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(nullable: false, maxLength: 10),
                        Date = c.DateTime(nullable: false),
                        Vat = c.Double(nullable: false),
                        Contract_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contract", t => t.Contract_Id)
                .Index(t => t.Contract_Id);
            
            CreateTable(
                "dbo.StandartMarginalIncome",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MarginalIncome = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StandartProductionTerm",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductionTerm = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sum",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Currency = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tender",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateOpen = c.DateTime(nullable: false),
                        DateClose = c.DateTime(nullable: false),
                        DateNotice = c.DateTime(),
                        Project_Id = c.Guid(nullable: false),
                        Winner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.Company", t => t.Winner_Id)
                .Index(t => t.Project_Id)
                .Index(t => t.Winner_Id);
            
            CreateTable(
                "dbo.TenderType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CompanyActivityField",
                c => new
                    {
                        Company_Id = c.Guid(nullable: false),
                        ActivityField_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Company_Id, t.ActivityField_Id })
                .ForeignKey("dbo.Company", t => t.Company_Id, cascadeDelete: true)
                .ForeignKey("dbo.ActivityField", t => t.ActivityField_Id, cascadeDelete: true)
                .Index(t => t.Company_Id)
                .Index(t => t.ActivityField_Id);
            
            CreateTable(
                "dbo.ParameterRelationParameter",
                c => new
                    {
                        ParameterRelation_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ParameterRelation_Id, t.Parameter_Id })
                .ForeignKey("dbo.ParameterRelation", t => t.ParameterRelation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.ParameterRelation_Id)
                .Index(t => t.Parameter_Id);
            
            CreateTable(
                "dbo.ProductBlockParameter",
                c => new
                    {
                        ProductBlock_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductBlock_Id, t.Parameter_Id })
                .ForeignKey("dbo.ProductBlock", t => t.ProductBlock_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.ProductBlock_Id)
                .Index(t => t.Parameter_Id);
            
            CreateTable(
                "dbo.DocumentEmployee",
                c => new
                    {
                        Document_Id = c.Guid(nullable: false),
                        Employee_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Document_Id, t.Employee_Id })
                .ForeignKey("dbo.Document", t => t.Document_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Document_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.UserUserRole",
                c => new
                    {
                        User_Id = c.Guid(nullable: false),
                        UserRole_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.UserRole_Id })
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserRole", t => t.UserRole_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.UserRole_Id);
            
            CreateTable(
                "dbo.PaymentConditionSetPaymentCondition",
                c => new
                    {
                        PaymentConditionSet_Id = c.Guid(nullable: false),
                        PaymentCondition_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PaymentConditionSet_Id, t.PaymentCondition_Id })
                .ForeignKey("dbo.PaymentConditionSet", t => t.PaymentConditionSet_Id, cascadeDelete: true)
                .ForeignKey("dbo.PaymentCondition", t => t.PaymentCondition_Id, cascadeDelete: true)
                .Index(t => t.PaymentConditionSet_Id)
                .Index(t => t.PaymentCondition_Id);
            
            CreateTable(
                "dbo.OfferUnitProductIncluded",
                c => new
                    {
                        OfferUnit_Id = c.Guid(nullable: false),
                        ProductIncluded_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.OfferUnit_Id, t.ProductIncluded_Id })
                .ForeignKey("dbo.OfferUnit", t => t.OfferUnit_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductIncluded", t => t.ProductIncluded_Id, cascadeDelete: true)
                .Index(t => t.OfferUnit_Id)
                .Index(t => t.ProductIncluded_Id);
            
            CreateTable(
                "dbo.ProductDesignationParameter",
                c => new
                    {
                        ProductDesignation_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductDesignation_Id, t.Parameter_Id })
                .ForeignKey("dbo.ProductDesignation", t => t.ProductDesignation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.ProductDesignation_Id)
                .Index(t => t.Parameter_Id);
            
            CreateTable(
                "dbo.ProductDesignationProductDesignation",
                c => new
                    {
                        ProductDesignation_Id = c.Guid(nullable: false),
                        ProductDesignation_Id1 = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductDesignation_Id, t.ProductDesignation_Id1 })
                .ForeignKey("dbo.ProductDesignation", t => t.ProductDesignation_Id)
                .ForeignKey("dbo.ProductDesignation", t => t.ProductDesignation_Id1)
                .Index(t => t.ProductDesignation_Id)
                .Index(t => t.ProductDesignation_Id1);
            
            CreateTable(
                "dbo.ProductRelationParameter",
                c => new
                    {
                        ProductRelation_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductRelation_Id, t.Parameter_Id })
                .ForeignKey("dbo.ProductRelation", t => t.ProductRelation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.ProductRelation_Id)
                .Index(t => t.Parameter_Id);
            
            CreateTable(
                "dbo.ProductRelationParameter1",
                c => new
                    {
                        ProductRelation_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductRelation_Id, t.Parameter_Id })
                .ForeignKey("dbo.ProductRelation", t => t.ProductRelation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.ProductRelation_Id)
                .Index(t => t.Parameter_Id);
            
            CreateTable(
                "dbo.ProductTypeDesignationParameter",
                c => new
                    {
                        ProductTypeDesignation_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductTypeDesignation_Id, t.Parameter_Id })
                .ForeignKey("dbo.ProductTypeDesignation", t => t.ProductTypeDesignation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.ProductTypeDesignation_Id)
                .Index(t => t.Parameter_Id);
            
            CreateTable(
                "dbo.SalesUnitLosingReason",
                c => new
                    {
                        SalesUnit_Id = c.Guid(nullable: false),
                        LosingReason_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SalesUnit_Id, t.LosingReason_Id })
                .ForeignKey("dbo.SalesUnit", t => t.SalesUnit_Id, cascadeDelete: true)
                .ForeignKey("dbo.LosingReason", t => t.LosingReason_Id, cascadeDelete: true)
                .Index(t => t.SalesUnit_Id)
                .Index(t => t.LosingReason_Id);
            
            CreateTable(
                "dbo.SalesUnitProductIncluded",
                c => new
                    {
                        SalesUnit_Id = c.Guid(nullable: false),
                        ProductIncluded_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SalesUnit_Id, t.ProductIncluded_Id })
                .ForeignKey("dbo.SalesUnit", t => t.SalesUnit_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductIncluded", t => t.ProductIncluded_Id, cascadeDelete: true)
                .Index(t => t.SalesUnit_Id)
                .Index(t => t.ProductIncluded_Id);
            
            CreateTable(
                "dbo.TenderCompany",
                c => new
                    {
                        Tender_Id = c.Guid(nullable: false),
                        Company_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tender_Id, t.Company_Id })
                .ForeignKey("dbo.Tender", t => t.Tender_Id, cascadeDelete: true)
                .ForeignKey("dbo.Company", t => t.Company_Id, cascadeDelete: true)
                .Index(t => t.Tender_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.TenderTenderType",
                c => new
                    {
                        Tender_Id = c.Guid(nullable: false),
                        TenderType_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tender_Id, t.TenderType_Id })
                .ForeignKey("dbo.Tender", t => t.Tender_Id, cascadeDelete: true)
                .ForeignKey("dbo.TenderType", t => t.TenderType_Id, cascadeDelete: true)
                .Index(t => t.Tender_Id)
                .Index(t => t.TenderType_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tender", "Winner_Id", "dbo.Company");
            DropForeignKey("dbo.TenderTenderType", "TenderType_Id", "dbo.TenderType");
            DropForeignKey("dbo.TenderTenderType", "Tender_Id", "dbo.Tender");
            DropForeignKey("dbo.Tender", "Project_Id", "dbo.Project");
            DropForeignKey("dbo.TenderCompany", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.TenderCompany", "Tender_Id", "dbo.Tender");
            DropForeignKey("dbo.Parameter", "StandartProductionTerm_Id", "dbo.StandartProductionTerm");
            DropForeignKey("dbo.Parameter", "StandartMarginalIncome_Id", "dbo.StandartMarginalIncome");
            DropForeignKey("dbo.SalesUnit", "Specification_Id", "dbo.Specification");
            DropForeignKey("dbo.Specification", "Contract_Id", "dbo.Contract");
            DropForeignKey("dbo.SalesUnit", "Project_Id", "dbo.Project");
            DropForeignKey("dbo.SalesUnitProductIncluded", "ProductIncluded_Id", "dbo.ProductIncluded");
            DropForeignKey("dbo.SalesUnitProductIncluded", "SalesUnit_Id", "dbo.SalesUnit");
            DropForeignKey("dbo.SalesUnit", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.SalesUnit", "Producer_Id", "dbo.Company");
            DropForeignKey("dbo.Penalty", "Id", "dbo.SalesUnit");
            DropForeignKey("dbo.PaymentPlanned", "SalesUnit_Id", "dbo.SalesUnit");
            DropForeignKey("dbo.PaymentActual", "SalesUnit_Id", "dbo.SalesUnit");
            DropForeignKey("dbo.SalesUnit", "PaymentConditionSet_Id", "dbo.PaymentConditionSet");
            DropForeignKey("dbo.SalesUnit", "Order_Id", "dbo.Order");
            DropForeignKey("dbo.SalesUnitLosingReason", "LosingReason_Id", "dbo.LosingReason");
            DropForeignKey("dbo.SalesUnitLosingReason", "SalesUnit_Id", "dbo.SalesUnit");
            DropForeignKey("dbo.FakeData", "Id", "dbo.SalesUnit");
            DropForeignKey("dbo.SalesUnit", "Facility_Id", "dbo.Facility");
            DropForeignKey("dbo.BankGuarantee", "SalesUnit_Id", "dbo.SalesUnit");
            DropForeignKey("dbo.SalesUnit", "AddressDelivery_Id", "dbo.Address");
            DropForeignKey("dbo.ProductType", "Id", "dbo.ProductTypeDesignation");
            DropForeignKey("dbo.ProductTypeDesignationParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.ProductTypeDesignationParameter", "ProductTypeDesignation_Id", "dbo.ProductTypeDesignation");
            DropForeignKey("dbo.ProductRelationParameter1", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.ProductRelationParameter1", "ProductRelation_Id", "dbo.ProductRelation");
            DropForeignKey("dbo.ProductRelationParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.ProductRelationParameter", "ProductRelation_Id", "dbo.ProductRelation");
            DropForeignKey("dbo.ProductDesignationProductDesignation", "ProductDesignation_Id1", "dbo.ProductDesignation");
            DropForeignKey("dbo.ProductDesignationProductDesignation", "ProductDesignation_Id", "dbo.ProductDesignation");
            DropForeignKey("dbo.ProductDesignationParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.ProductDesignationParameter", "ProductDesignation_Id", "dbo.ProductDesignation");
            DropForeignKey("dbo.PaymentPlanned", "Condition_Id", "dbo.PaymentCondition");
            DropForeignKey("dbo.PaymentActual", "PaymentDocument_Id", "dbo.PaymentDocument");
            DropForeignKey("dbo.OfferUnitProductIncluded", "ProductIncluded_Id", "dbo.ProductIncluded");
            DropForeignKey("dbo.OfferUnitProductIncluded", "OfferUnit_Id", "dbo.OfferUnit");
            DropForeignKey("dbo.ProductIncluded", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.OfferUnit", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.OfferUnit", "PaymentConditionSet_Id", "dbo.PaymentConditionSet");
            DropForeignKey("dbo.OfferUnit", "Offer_Id", "dbo.Document");
            DropForeignKey("dbo.OfferUnit", "Facility_Id", "dbo.Facility");
            DropForeignKey("dbo.ActivityField", "MarketField_Id", "dbo.MarketField");
            DropForeignKey("dbo.GlobalProperties", "VoltageGroup_Id", "dbo.ParameterGroup");
            DropForeignKey("dbo.GlobalProperties", "SupervisionParameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.GlobalProperties", "StandartPaymentsConditionSet_Id", "dbo.PaymentConditionSet");
            DropForeignKey("dbo.GlobalProperties", "ServiceParameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.GlobalProperties", "SenderOfferEmployee_Id", "dbo.Employee");
            DropForeignKey("dbo.GlobalProperties", "PaymentConditionSet_Id", "dbo.PaymentConditionSet");
            DropForeignKey("dbo.GlobalProperties", "OurCompany_Id", "dbo.Company");
            DropForeignKey("dbo.GlobalProperties", "NewProductParameterGroup_Id", "dbo.ParameterGroup");
            DropForeignKey("dbo.GlobalProperties", "NewProductParameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.GlobalProperties", "HvtProducersActivityField_Id", "dbo.ActivityField");
            DropForeignKey("dbo.FakeData", "PaymentConditionSet_Id", "dbo.PaymentConditionSet");
            DropForeignKey("dbo.PaymentConditionSetPaymentCondition", "PaymentCondition_Id", "dbo.PaymentCondition");
            DropForeignKey("dbo.PaymentConditionSetPaymentCondition", "PaymentConditionSet_Id", "dbo.PaymentConditionSet");
            DropForeignKey("dbo.PaymentCondition", "PaymentConditionPoint_Id", "dbo.PaymentConditionPoint");
            DropForeignKey("dbo.Facility", "Type_Id", "dbo.FacilityType");
            DropForeignKey("dbo.Facility", "OwnerCompany_Id", "dbo.Company");
            DropForeignKey("dbo.Facility", "Address_Id", "dbo.Address");
            DropForeignKey("dbo.Document", "Project_Id", "dbo.Project");
            DropForeignKey("dbo.Project", "ProjectType_Id", "dbo.ProjectType");
            DropForeignKey("dbo.Note", "Project_Id", "dbo.Project");
            DropForeignKey("dbo.Project", "Manager_Id", "dbo.User");
            DropForeignKey("dbo.UserUserRole", "UserRole_Id", "dbo.UserRole");
            DropForeignKey("dbo.UserUserRole", "User_Id", "dbo.User");
            DropForeignKey("dbo.User", "Id", "dbo.Employee");
            DropForeignKey("dbo.Document", "SenderId", "dbo.Employee");
            DropForeignKey("dbo.Document", "RequestDocument_Id", "dbo.Document");
            DropForeignKey("dbo.DocumentsRegistrationDetails", "Document_Id", "dbo.Document");
            DropForeignKey("dbo.Document", "RecipientId", "dbo.Employee");
            DropForeignKey("dbo.Document", "Number_Number", "dbo.DocumentNumber");
            DropForeignKey("dbo.DocumentEmployee", "Employee_Id", "dbo.Employee");
            DropForeignKey("dbo.DocumentEmployee", "Document_Id", "dbo.Document");
            DropForeignKey("dbo.Document", "Author_Id", "dbo.Employee");
            DropForeignKey("dbo.Employee", "Position_Id", "dbo.EmployeesPosition");
            DropForeignKey("dbo.Employee", "Person_Id", "dbo.Person");
            DropForeignKey("dbo.Employee", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.CreateNewProductTask", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Product", "ProductBlock_Id", "dbo.ProductBlock");
            DropForeignKey("dbo.SumOnDate", "ProductBlock_Id1", "dbo.ProductBlock");
            DropForeignKey("dbo.ProductBlockParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.ProductBlockParameter", "ProductBlock_Id", "dbo.ProductBlock");
            DropForeignKey("dbo.ParameterRelation", "ParameterId", "dbo.Parameter");
            DropForeignKey("dbo.ParameterRelationParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.ParameterRelationParameter", "ParameterRelation_Id", "dbo.ParameterRelation");
            DropForeignKey("dbo.Parameter", "ParameterGroup_Id", "dbo.ParameterGroup");
            DropForeignKey("dbo.ParameterGroup", "Measure_Id", "dbo.Measure");
            DropForeignKey("dbo.SumOnDate", "ProductBlock_Id", "dbo.ProductBlock");
            DropForeignKey("dbo.ProductDependent", "MainProductId", "dbo.Product");
            DropForeignKey("dbo.ProductDependent", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Country", "CountryUnion_Id", "dbo.CountryUnion");
            DropForeignKey("dbo.Contract", "Contragent_Id", "dbo.Company");
            DropForeignKey("dbo.Company", "ParentCompany_Id", "dbo.Company");
            DropForeignKey("dbo.Company", "Form_Id", "dbo.CompanyForm");
            DropForeignKey("dbo.BankDetails", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.Company", "AddressPost_Id", "dbo.Address");
            DropForeignKey("dbo.Company", "AddressLegal_Id", "dbo.Address");
            DropForeignKey("dbo.CompanyActivityField", "ActivityField_Id", "dbo.ActivityField");
            DropForeignKey("dbo.CompanyActivityField", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.BankGuarantee", "BankGuaranteeType_Id", "dbo.BankGuaranteeType");
            DropForeignKey("dbo.Address", "Locality_Id", "dbo.Locality");
            DropForeignKey("dbo.Locality", "Region_Id", "dbo.Region");
            DropForeignKey("dbo.Region", "District_Id", "dbo.District");
            DropForeignKey("dbo.District", "Country_Id", "dbo.Country");
            DropForeignKey("dbo.Locality", "LocalityType_Id", "dbo.LocalityType");
            DropIndex("dbo.TenderTenderType", new[] { "TenderType_Id" });
            DropIndex("dbo.TenderTenderType", new[] { "Tender_Id" });
            DropIndex("dbo.TenderCompany", new[] { "Company_Id" });
            DropIndex("dbo.TenderCompany", new[] { "Tender_Id" });
            DropIndex("dbo.SalesUnitProductIncluded", new[] { "ProductIncluded_Id" });
            DropIndex("dbo.SalesUnitProductIncluded", new[] { "SalesUnit_Id" });
            DropIndex("dbo.SalesUnitLosingReason", new[] { "LosingReason_Id" });
            DropIndex("dbo.SalesUnitLosingReason", new[] { "SalesUnit_Id" });
            DropIndex("dbo.ProductTypeDesignationParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.ProductTypeDesignationParameter", new[] { "ProductTypeDesignation_Id" });
            DropIndex("dbo.ProductRelationParameter1", new[] { "Parameter_Id" });
            DropIndex("dbo.ProductRelationParameter1", new[] { "ProductRelation_Id" });
            DropIndex("dbo.ProductRelationParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.ProductRelationParameter", new[] { "ProductRelation_Id" });
            DropIndex("dbo.ProductDesignationProductDesignation", new[] { "ProductDesignation_Id1" });
            DropIndex("dbo.ProductDesignationProductDesignation", new[] { "ProductDesignation_Id" });
            DropIndex("dbo.ProductDesignationParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.ProductDesignationParameter", new[] { "ProductDesignation_Id" });
            DropIndex("dbo.OfferUnitProductIncluded", new[] { "ProductIncluded_Id" });
            DropIndex("dbo.OfferUnitProductIncluded", new[] { "OfferUnit_Id" });
            DropIndex("dbo.PaymentConditionSetPaymentCondition", new[] { "PaymentCondition_Id" });
            DropIndex("dbo.PaymentConditionSetPaymentCondition", new[] { "PaymentConditionSet_Id" });
            DropIndex("dbo.UserUserRole", new[] { "UserRole_Id" });
            DropIndex("dbo.UserUserRole", new[] { "User_Id" });
            DropIndex("dbo.DocumentEmployee", new[] { "Employee_Id" });
            DropIndex("dbo.DocumentEmployee", new[] { "Document_Id" });
            DropIndex("dbo.ProductBlockParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.ProductBlockParameter", new[] { "ProductBlock_Id" });
            DropIndex("dbo.ParameterRelationParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.ParameterRelationParameter", new[] { "ParameterRelation_Id" });
            DropIndex("dbo.CompanyActivityField", new[] { "ActivityField_Id" });
            DropIndex("dbo.CompanyActivityField", new[] { "Company_Id" });
            DropIndex("dbo.Tender", new[] { "Winner_Id" });
            DropIndex("dbo.Tender", new[] { "Project_Id" });
            DropIndex("dbo.Specification", new[] { "Contract_Id" });
            DropIndex("dbo.SalesUnit", new[] { "Specification_Id" });
            DropIndex("dbo.SalesUnit", new[] { "Project_Id" });
            DropIndex("dbo.SalesUnit", new[] { "Product_Id" });
            DropIndex("dbo.SalesUnit", new[] { "Producer_Id" });
            DropIndex("dbo.SalesUnit", new[] { "PaymentConditionSet_Id" });
            DropIndex("dbo.SalesUnit", new[] { "Order_Id" });
            DropIndex("dbo.SalesUnit", new[] { "Facility_Id" });
            DropIndex("dbo.SalesUnit", new[] { "AddressDelivery_Id" });
            DropIndex("dbo.ProductType", new[] { "Id" });
            DropIndex("dbo.Penalty", new[] { "Id" });
            DropIndex("dbo.PaymentPlanned", new[] { "SalesUnit_Id" });
            DropIndex("dbo.PaymentPlanned", new[] { "Condition_Id" });
            DropIndex("dbo.PaymentActual", new[] { "SalesUnit_Id" });
            DropIndex("dbo.PaymentActual", new[] { "PaymentDocument_Id" });
            DropIndex("dbo.ProductIncluded", new[] { "Product_Id" });
            DropIndex("dbo.OfferUnit", new[] { "Product_Id" });
            DropIndex("dbo.OfferUnit", new[] { "PaymentConditionSet_Id" });
            DropIndex("dbo.OfferUnit", new[] { "Offer_Id" });
            DropIndex("dbo.OfferUnit", new[] { "Facility_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "VoltageGroup_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "SupervisionParameter_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "StandartPaymentsConditionSet_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "ServiceParameter_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "SenderOfferEmployee_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "PaymentConditionSet_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "OurCompany_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "NewProductParameterGroup_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "NewProductParameter_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "HvtProducersActivityField_Id" });
            DropIndex("dbo.PaymentCondition", new[] { "PaymentConditionPoint_Id" });
            DropIndex("dbo.FakeData", new[] { "PaymentConditionSet_Id" });
            DropIndex("dbo.FakeData", new[] { "Id" });
            DropIndex("dbo.Facility", new[] { "Type_Id" });
            DropIndex("dbo.Facility", new[] { "OwnerCompany_Id" });
            DropIndex("dbo.Facility", new[] { "Address_Id" });
            DropIndex("dbo.Note", new[] { "Project_Id" });
            DropIndex("dbo.User", new[] { "Id" });
            DropIndex("dbo.Project", new[] { "ProjectType_Id" });
            DropIndex("dbo.Project", new[] { "Manager_Id" });
            DropIndex("dbo.DocumentsRegistrationDetails", new[] { "Document_Id" });
            DropIndex("dbo.Employee", new[] { "Position_Id" });
            DropIndex("dbo.Employee", new[] { "Person_Id" });
            DropIndex("dbo.Employee", new[] { "Company_Id" });
            DropIndex("dbo.Document", new[] { "Project_Id" });
            DropIndex("dbo.Document", new[] { "RequestDocument_Id" });
            DropIndex("dbo.Document", new[] { "Number_Number" });
            DropIndex("dbo.Document", new[] { "Author_Id" });
            DropIndex("dbo.Document", new[] { "RecipientId" });
            DropIndex("dbo.Document", new[] { "SenderId" });
            DropIndex("dbo.ParameterRelation", new[] { "ParameterId" });
            DropIndex("dbo.ParameterGroup", new[] { "Measure_Id" });
            DropIndex("dbo.Parameter", new[] { "StandartProductionTerm_Id" });
            DropIndex("dbo.Parameter", new[] { "StandartMarginalIncome_Id" });
            DropIndex("dbo.Parameter", new[] { "ParameterGroup_Id" });
            DropIndex("dbo.SumOnDate", new[] { "ProductBlock_Id1" });
            DropIndex("dbo.SumOnDate", new[] { "ProductBlock_Id" });
            DropIndex("dbo.ProductDependent", new[] { "Product_Id" });
            DropIndex("dbo.ProductDependent", new[] { "MainProductId" });
            DropIndex("dbo.Product", new[] { "ProductBlock_Id" });
            DropIndex("dbo.CreateNewProductTask", new[] { "Product_Id" });
            DropIndex("dbo.Contract", new[] { "Contragent_Id" });
            DropIndex("dbo.Company", new[] { "ParentCompany_Id" });
            DropIndex("dbo.Company", new[] { "Form_Id" });
            DropIndex("dbo.Company", new[] { "AddressPost_Id" });
            DropIndex("dbo.Company", new[] { "AddressLegal_Id" });
            DropIndex("dbo.BankGuarantee", new[] { "SalesUnit_Id" });
            DropIndex("dbo.BankGuarantee", new[] { "BankGuaranteeType_Id" });
            DropIndex("dbo.BankDetails", new[] { "Company_Id" });
            DropIndex("dbo.Country", new[] { "CountryUnion_Id" });
            DropIndex("dbo.District", new[] { "Country_Id" });
            DropIndex("dbo.Region", new[] { "District_Id" });
            DropIndex("dbo.Locality", new[] { "Region_Id" });
            DropIndex("dbo.Locality", new[] { "LocalityType_Id" });
            DropIndex("dbo.Address", new[] { "Locality_Id" });
            DropIndex("dbo.ActivityField", new[] { "MarketField_Id" });
            DropTable("dbo.TenderTenderType");
            DropTable("dbo.TenderCompany");
            DropTable("dbo.SalesUnitProductIncluded");
            DropTable("dbo.SalesUnitLosingReason");
            DropTable("dbo.ProductTypeDesignationParameter");
            DropTable("dbo.ProductRelationParameter1");
            DropTable("dbo.ProductRelationParameter");
            DropTable("dbo.ProductDesignationProductDesignation");
            DropTable("dbo.ProductDesignationParameter");
            DropTable("dbo.OfferUnitProductIncluded");
            DropTable("dbo.PaymentConditionSetPaymentCondition");
            DropTable("dbo.UserUserRole");
            DropTable("dbo.DocumentEmployee");
            DropTable("dbo.ProductBlockParameter");
            DropTable("dbo.ParameterRelationParameter");
            DropTable("dbo.CompanyActivityField");
            DropTable("dbo.TenderType");
            DropTable("dbo.Tender");
            DropTable("dbo.Sum");
            DropTable("dbo.StandartProductionTerm");
            DropTable("dbo.StandartMarginalIncome");
            DropTable("dbo.Specification");
            DropTable("dbo.SalesUnit");
            DropTable("dbo.ProductTypeDesignation");
            DropTable("dbo.ProductType");
            DropTable("dbo.ProductRelation");
            DropTable("dbo.ProductDesignation");
            DropTable("dbo.Penalty");
            DropTable("dbo.PaymentPlanned");
            DropTable("dbo.PaymentDocument");
            DropTable("dbo.PaymentActual");
            DropTable("dbo.Order");
            DropTable("dbo.ProductIncluded");
            DropTable("dbo.OfferUnit");
            DropTable("dbo.MarketField");
            DropTable("dbo.LosingReason");
            DropTable("dbo.GlobalProperties");
            DropTable("dbo.PaymentConditionPoint");
            DropTable("dbo.PaymentCondition");
            DropTable("dbo.PaymentConditionSet");
            DropTable("dbo.FakeData");
            DropTable("dbo.FacilityType");
            DropTable("dbo.Facility");
            DropTable("dbo.ProjectType");
            DropTable("dbo.Note");
            DropTable("dbo.UserRole");
            DropTable("dbo.User");
            DropTable("dbo.Project");
            DropTable("dbo.DocumentsRegistrationDetails");
            DropTable("dbo.DocumentNumber");
            DropTable("dbo.EmployeesPosition");
            DropTable("dbo.Person");
            DropTable("dbo.Employee");
            DropTable("dbo.Document");
            DropTable("dbo.CurrencyExchangeRate");
            DropTable("dbo.ParameterRelation");
            DropTable("dbo.Measure");
            DropTable("dbo.ParameterGroup");
            DropTable("dbo.Parameter");
            DropTable("dbo.SumOnDate");
            DropTable("dbo.ProductBlock");
            DropTable("dbo.ProductDependent");
            DropTable("dbo.Product");
            DropTable("dbo.CreateNewProductTask");
            DropTable("dbo.CountryUnion");
            DropTable("dbo.Contract");
            DropTable("dbo.CompanyForm");
            DropTable("dbo.Company");
            DropTable("dbo.BankGuaranteeType");
            DropTable("dbo.BankGuarantee");
            DropTable("dbo.BankDetails");
            DropTable("dbo.Country");
            DropTable("dbo.District");
            DropTable("dbo.Region");
            DropTable("dbo.LocalityType");
            DropTable("dbo.Locality");
            DropTable("dbo.Address");
            DropTable("dbo.ActivityField");
        }
    }
}
