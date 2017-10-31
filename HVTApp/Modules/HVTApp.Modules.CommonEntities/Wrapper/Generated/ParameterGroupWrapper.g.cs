using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class ParameterGroupWrapper : WrapperBase<ParameterGroup>
	{
	public ParameterGroupWrapper(ParameterGroup model) : base(model) { }

	
    #region SimpleProperties
    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	private MeasureWrapper _fieldMeasure;
	public MeasureWrapper Measure 
    {
        get { return _fieldMeasure ; }
        set
        {
            SetComplexValue<Measure, MeasureWrapper>(_fieldMeasure, value);
            _fieldMeasure  = value;
        }
    }
    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }

    #endregion
    public override void InitializeComplexProperties()
    {
		if (Model.Measure != null)
        {
            _fieldMeasure = new MeasureWrapper(Model.Measure);
            RegisterComplex(Measure);
        }
    }
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
      Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
      RegisterCollection(Parameters, Model.Parameters);

    }
	}
}
	