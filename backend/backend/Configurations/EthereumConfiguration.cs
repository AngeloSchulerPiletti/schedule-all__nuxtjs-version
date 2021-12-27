namespace backend.Configurations
{
    public class EthereumConfiguration
    {
        public string Url { get; set; }
        public string Port { get; set; }
        public string TaskTokenContractAddress { get; set; }
        public string Host {
            get => $"{Url}:{Port}";
        }
    }
}
