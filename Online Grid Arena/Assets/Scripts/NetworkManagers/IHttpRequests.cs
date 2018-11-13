using System.Collections;

public interface IHttpRequests
{
    IEnumerator Post(string url, string body);
}
