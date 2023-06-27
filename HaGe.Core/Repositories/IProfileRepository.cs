﻿using HaGe.Core.Entities;
using HaGe.Core.Repositories.Base;

namespace HaGe.Core.Repositories; 

public interface IProfileRepository : IRepository<Profile> {
    int GetUserLevel(Guid profileId);
    Profile GetByUserId(Guid modelLoggedUserId);
}