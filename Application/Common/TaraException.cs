

namespace Application.Common;

public class TaraException: Exception
{
    public TaraException()
    {
        
    }
    public TaraException(string message):base(message) 
    {
        
    }
    public TaraException(string message,Exception exception):base(message,exception) 
    {
        
    }
}
