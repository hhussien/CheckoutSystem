namespace Domain.DataAccess.Configurations.Interfaces
{
	public interface IConfigurationSet
	{
        IConfigurationCollection GetConfigurations(string configCategory);
        
	}

} 