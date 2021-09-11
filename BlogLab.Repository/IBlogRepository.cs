﻿using BlogLabModels.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogLab.Repository
{
    public interface IBlogrepository    
    {
        public Task<Blog> UpsertAsync(BlogCreate blogCreate, int applicationUserId);
        public Task<PageResults<Blog>> GetAllAsync(BlogPaging blogPaging);
        public Task<Blog> GetAsync(int applicationUserId);
        public Task<List<Blog>> GetAllByUserIdAsync(int applicationUserId);
        public Task<List<Blog>> GetAllFamousAsync();

        public Task<int> DeleteAsync(int blogId);
    }
}
