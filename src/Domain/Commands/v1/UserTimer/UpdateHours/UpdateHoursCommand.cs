namespace Domain.Commands.v1.UserTimer.UpdateHours;
public sealed class UpdateHoursCommand : IRequest<Unit>
{
    public string Email { get; set; }
    public double Hour { get; set; }
    public string Remark { get; set; }
}
