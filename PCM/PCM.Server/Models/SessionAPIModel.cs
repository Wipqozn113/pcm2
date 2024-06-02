using Microsoft.Extensions.Caching.Memory;
using PCM.Server.APIQueryService.Models;
using PCM.Server.APIQueryService.Service;
using System.Diagnostics.Metrics;

namespace PCM.Server.Models
{
    public class SessionAPIModel
    {
        private SessionAPIModel() { }
        private static MemoryCache Cache { get; set; }
            = new MemoryCache(new MemoryCacheOptions());

        public static SessionAPIModel Create(QueryService apiQueryService, int sessionId)
        {
            // Return a cached version if it exists
            var cachedEncounter = Cache.Get<SessionAPIModel>(sessionId);
            if (cachedEncounter is not null)
                return cachedEncounter;

            var session = apiQueryService.GetSession(sessionId);
            if (session is null)
                return new SessionAPIModel();

            return CreateSessionAPIModel(apiQueryService, session);
        }

        public static SessionAPIModel GetNextSession(QueryService apiQueryService)
        {
            // Return a cached version if it exists
            var cachedEncounter = Cache.Get<SessionAPIModel>(DateOnly.FromDateTime(DateTime.Now));
            if (cachedEncounter is not null)
                return cachedEncounter;

            var session = apiQueryService.GetNextSession();
            if (session is null)
                return new SessionAPIModel();

            var model = CreateSessionAPIModel(apiQueryService, session);
            Cache.Set(DateOnly.FromDateTime(DateTime.Now), model);

            return model;
        }

        private static SessionAPIModel CreateSessionAPIModel(QueryService apiQueryService,  Session session)
        {
            var model = new SessionAPIModel()
            {
                Id = session.Id,
                SessionDate = session.SessionDateOnly,
                Overview = session.Overview,
                Notes = session.Notes
            };

            // Populate list of loot
            foreach (var lootId in session.Loot)
            {
                var loot = LootAPIModel.Create(apiQueryService, lootId);

                if (loot is not null)
                    model.Loot.Add(loot);
            }

            // Populate list of encounters
            foreach (var encounterId in session.Encounters)
            {
                var encounter = EncounterAPIModel.Create(apiQueryService, encounterId);

                if (encounter is not null)
                    model.Encounters.Add(encounter);
            }

            Cache.Set(session.Id, model);

            return model;
        }

        public int Id { get; set; }

        public DateOnly SessionDate { get; set; } 

        public string Overview { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public List<LootAPIModel> Loot { get; set; } = new List<LootAPIModel>();

        public List<EncounterAPIModel> Encounters { get; set; } = new List<EncounterAPIModel>();
    }
}
