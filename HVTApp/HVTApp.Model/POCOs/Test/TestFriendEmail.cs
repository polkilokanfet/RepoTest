using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
  public partial class TestFriendEmail : BaseEntity
    {
    public string Email { get; set; }
    public string Comment { get; set; }
  }
}