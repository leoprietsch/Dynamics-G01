using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Zup.Lab.Plugins.Model;

namespace Zup.Lab.Plugins.Business {
    public class PostBusiness {

        public Entity getPostById(Guid id)
        {
            int postId = Guid2Int(id);
            var url = $"https://jsonplaceholder.typicode.com/posts/{postId}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = new HttpClient().SendAsync(request).GetAwaiter().GetResult();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new InvalidPluginExecutionException("Erro ao buscar post");

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var post = JsonConvert.DeserializeObject<Post>(content);

            var postEntity = new Entity("zup_virtual_post", id);
            postEntity["zup_virtual_postid"] = Int2Guid(post.id);
            postEntity["zup_userid"] = post.userId.ToString();
            postEntity["zup_name"] = post.title;
            postEntity["zup_body"] = post.body;
            return postEntity;
        }

        public EntityCollection getPosts()
        {
            var url = $"https://jsonplaceholder.typicode.com/posts";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = new HttpClient().SendAsync(request).GetAwaiter().GetResult();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new InvalidPluginExecutionException("Erro ao buscar post");

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var posts = JsonConvert.DeserializeObject<List<Post>>(content);

            EntityCollection ec = new EntityCollection();

            foreach (var post in posts)
            {
                var postEntity = new Entity("zup_virtual_post", Int2Guid(post.id));
                postEntity["zup_virtual_postid"] = Int2Guid(post.id);
                postEntity["zup_userid"] = post.userId.ToString();
                postEntity["zup_name"] = post.title;
                postEntity["zup_body"] = post.body;

                ec.Entities.Add(postEntity);
            }

            return ec;
        }

        public static Guid Int2Guid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public static int Guid2Int(Guid value)
        {
            byte[] b = value.ToByteArray();
            int bint = BitConverter.ToInt32(b, 0);
            return bint;
        }
    }
}
