using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsLive.Writer.Api;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Net;
using System.Collections.Specialized;

namespace GistPlugin
{    
    [WriterPluginAttribute
         ("1C6664BB-D27E-4a99-AE73-E986DF364F15",
         "GIST Snippet",
         ImagePath = "Images.paste.png",
         PublisherUrl = "http://www.kodermonkeys.com",
         Description = "Helps you to embed Gist snippers from GitHub in your blog posts")]


    [InsertableContentSourceAttribute("GIST Snippet")]
    public class Plugin : ContentSource
    {
        public override DialogResult CreateContent(IWin32Window dialogOwner,
            ref string newContent)
        {
            using (GistForm insertForm = new GistForm())
            {
                DialogResult result = insertForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    String s = this.GetOutput(insertForm.Output, insertForm.Filename);
                    if(s != null)
                        newContent = s;
                    else 
                        return DialogResult.Cancel;
                }
                return result;
            }
        }

        string API_URL    = "http://gist.github.com/api/v1/json/new";



        private string GetOutput(string res, string file_name)
        {
            try {
			
       	        WebClient client = new WebClient();
		        NameValueCollection form = new NameValueCollection();

                form.Add("login", Properties.Settings.Default.login);
                form.Add("token", Properties.Settings.Default.api);
                form.Add(String.Format("file_ext[{0}]", file_name), Path.GetExtension(file_name));
		        form.Add(String.Format("file_name[{0}]", file_name),file_name );
		        form.Add(String.Format("file_contents[{0}]", file_name), res);
		        byte[] responseData = client.UploadValues(API_URL, form);
                String s =  Encoding.UTF8.GetString(responseData);
                System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("\"repo\":\"(\\d+)\"");
                System.Text.RegularExpressions.Match m = r.Match(s);

                if(m.Success){
                    return String.Format("<script src=\"http://gist.github.com/{0}.js\"></script>", m.Groups[1].Value);
                }
                return null;
            }
            catch (Exception webEx) {
          	    Console.WriteLine(webEx.ToString());

            }

            return null;
        }
    }
}
