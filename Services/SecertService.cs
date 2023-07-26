using System.Text;
using Google.Api.Gax.ResourceNames;
using Google.Cloud.SecretManager.V1;
using Google.Protobuf;
using Newtonsoft.Json;

public class SecertService
{
    public string GetSecret(string projectId, string secretId, string versionId = "latest")
    {
        // Create the client.
        SecretManagerServiceClient client = SecretManagerServiceClient.Create();

        // Build the secret version name.
        SecretVersionName secretVersionName = new SecretVersionName(projectId, secretId, versionId);

        // Access the secret value.
        AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);

        // Decode the secret value from the response.
        string secretValue = result.Payload.Data.ToStringUtf8();

        return secretValue;
    }
}

