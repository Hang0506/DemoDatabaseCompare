public class CassandraSettings
{
    public string[] ContactPoints { get; set; } = [];
    public int Port { get; set; }
    public string Keyspace { get; set; } = string.Empty;
} 