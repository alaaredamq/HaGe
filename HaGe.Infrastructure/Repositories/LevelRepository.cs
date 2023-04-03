﻿using HaGe.Core.Entities;
using HaGe.Core.Repositories;
using HaGe.Infrastructure.Context;
using HaGe.Infrastructure.Repositories.Base;

namespace HaGe.Infrastructure.Repositories; 

public class LevelRepository: Repository<Level>, ILevelRepository {
    public LevelRepository(HaGeContext dbContext)
        : base(dbContext) {
    }
    
}