using GitHubClient.WebApi.Entities;
using GitHubClient.WebApi.Models.Reponse;

namespace GitHubClient.WebApi.Mappers
{
    public class CommitMapper
    {
        public static Commit MapToEntity(CommitsResponseModel model)
        {
            var result = new Commit();
            result.Author = model.Commit.Author;
            result.Committer = model.Commit.Committer;
            result.Message = model.Commit.Message;
            result.SHA = model.SHA;
            return result;
        }
    }
}
