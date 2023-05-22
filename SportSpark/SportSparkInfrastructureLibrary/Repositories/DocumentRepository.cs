using SportSparkCoreLibrary.Entities;
using SportSparkInfrastructureLibrary.Database;
using SportSparkInfrastructureLibrary.Repositories.Base;
using System;
using System.Collections.Generic;
namespace SportSparkInfrastructureLibrary.Repositories
{
    public class DocumentRepository : BaseRepository<Document>
    {
        public DocumentRepository(SportSparkDBContext context) : base(context)
        {
        }
    }
}
