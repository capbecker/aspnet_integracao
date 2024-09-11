namespace Backend.API.Models
{
    public class Resposta<T>
    {
        public Resposta()
        {
        }
        public Resposta(T data)
        {
            Data = data;
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            
        }
        public Resposta(T data, Dictionary<string, object> map)
        {
            Data = data;
            Succeeded = (bool) map.GetValueOrDefault("Succed", true);
            Message = (string?)map.GetValueOrDefault("Message", string.Empty); 
            Errors = (string[]?)map.GetValueOrDefault("Errors", null);           
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}