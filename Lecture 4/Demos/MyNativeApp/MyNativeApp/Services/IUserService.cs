namespace MyNativeApp
{
	public interface IUserService
	{
		string UserEmail { get; set; }

		string TeamId { get; }

		string UserId { get; }
	}
}