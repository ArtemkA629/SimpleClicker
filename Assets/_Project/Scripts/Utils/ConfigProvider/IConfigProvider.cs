public interface IConfigProvider
{ 
    T Get<T>() where T : class;
}