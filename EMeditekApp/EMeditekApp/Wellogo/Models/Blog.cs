using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{

    public class BlogFeatured
    {
        public string status { get; set; }
        public string message { get; set; }
        public BlogData data { get; set; }
    }

    
    public class FeaturedPivot
    {
        public int blog_id { get; set; }
        public int tag_id { get; set; }
        public int id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class Tag
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public FeaturedPivot pivot { get; set; }
    }

    public class CategoryPivot  
    {
        public int blog_id { get; set; }
        public int category_id { get; set; }
        public int id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class Category  : CategoryData
    {
        public CategoryPivot pivot { get; set; }
    }

    public class Photo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string mime_type { get; set; }
        public string is_active { get; set; }
        public int imageable_id { get; set; }
        public string imageable_type { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class BlogData
    {
        public int id { get; set; }
        public string title { get; set; }
        public string sub_title { get; set; }
        public string content { get; set; }
        public string author { get; set; }
        public int posted_by { get; set; }
        public int status { get; set; }
        public int is_featured { get; set; }
        public string post_date_time { get; set; }
        public string slug { get; set; }
        public DateTime created_at { get; set; }
        public string updated_at { get; set; }
        public string short_content { get; set; }
        public string blog_photo { get; set; }
        public List<Tag> tags { get; set; }
        public List<Category> categories { get; set; }
        public Photo photo { get; set; }
    }

    public class AllBlogs
    {
        public string status { get; set; }
        public string message { get; set; }
        public AllBlogsPageData data { get; set; }
    }

    public class AllBlogsPageData
    {

        public int total { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int last_page { get; set; }
        public object next_page_url { get; set; }
        public object prev_page_url { get; set; }
        public  int? from { get; set; }
        public  int?  to { get; set; }
        public List<BlogData> data { get; set; }

    }


    public class CategoryData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string is_active { get; set; }
        public string slug { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class Allcategories
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<CategoryData> data { get; set; }
    }














}
