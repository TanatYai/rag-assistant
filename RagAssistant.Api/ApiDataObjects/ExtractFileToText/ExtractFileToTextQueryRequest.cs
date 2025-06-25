using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RagAssistant.Api.ApiDataObjects.ExtractFileToText
{
    public class ExtractFileToTextQueryRequest : IRequest<ExtractFileToTextQueryResponse>
    {
        [JsonPropertyName("file_name")]
        [Required]
        public string FileName { get; set; } = default!;

        [JsonPropertyName("file_download_url")]
        [Required]
        public string FileDownloadUrl { get; set; } = default!;
    }
}
