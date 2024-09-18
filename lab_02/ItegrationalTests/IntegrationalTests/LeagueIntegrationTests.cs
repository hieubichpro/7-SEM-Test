using ItegrationalTests.Fixture;
using lab_03.BL.Services;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItegrationalTests.IntegrationalTests
{
    public class LeagueIntegrationTests : IntegrationFixture
    {
        private LeagueService _leagueService;
        public LeagueIntegrationTests()
        {
            _leagueService = new LeagueService(_leagueRepo, _matchRepo, _clubRepo, null, NullLogger<LeagueService>.Instance);
        }

    }
}
