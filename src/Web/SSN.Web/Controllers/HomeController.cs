namespace SSN.Web.Controllers
{
    using System.Diagnostics;

    using SSN.Web.ViewModels.Users;

    using Microsoft.AspNetCore.Mvc;
    using SSN.Services.Data;
    using SSN.Web.ViewModels;
    using SSN.Web.ViewModels.UserAcc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using System;

    public class HomeController : BaseController
    {
        private const int ItemPerPage = 10;

        private readonly IUserAccService userAccService; 
        private readonly IVotesService votesService;

        public HomeController(IUserAccService userAccService,
                              IVotesService votesService)
        {
            this.userAccService = userAccService; 
            this.votesService = votesService;
        }

        public IActionResult Index()
        {
            var mostVotedDocId = this.userAccService.MostVotedDoc();
            var mostCommentDocId = this.userAccService.MostCommentDoc();

            var mostVotedDoctors = new UsersAccViewModel
            {
                Users = this.userAccService.GetMostVoted<UserAccViewModel>(mostVotedDocId),
                Doc = this.userAccService.GetMostVoted<UserAccViewModel>(mostCommentDocId),
            };

            return this.View(mostVotedDoctors);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
