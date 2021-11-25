using System.Collections.Generic;
using Proyecto26;
using SimpleJSON;
using UnityEngine;

namespace Script.Scoreboard.Firebase
{
    public class TestFirebase : MonoBehaviour
    {
        public string url = "https://testfirebase1620702256-default-rtdb.asia-southeast1.firebasedatabase.app";
        public string secret = "MjoyQ5jQiV4C9UI6sI3WNNj5Osoy6fnMmouEYv6h";

        [System.Serializable]
        public class User
        {
            [System.Serializable]
            public class UserData
            {
                public string name;
                public string gender;
                public int age;

                public UserData(string name, string gender, int age)
                {
                    this.name = name;
                    this.gender = gender;
                    this.age = age;
                }
            }
            public List<UserData> userData;

        }

        public User user;

  

        // Start is called before the first frame update
        void Start()
        {
            GetData();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void GetData()
        {
            string urlData = $"{url}/User.json?auth={secret}";

            RestClient.Get(urlData).Then(response =>
            {
                Debug.Log(response.Text);
                JSONNode jsonNode = JSONNode.Parse(response.Text);

                for (int i = 0; i < jsonNode.Count; i++)
           
                {
           
                    user.userData.Add(new User.UserData(jsonNode[i]["name"], jsonNode[i]["gender"], jsonNode[i]["age"]));
           
                }
           
                //user = new User(jsonNode["name"],jsonNode["gender"],jsonNode["age"]);

            }).Catch(error =>
            {

                Debug.Log("error");
            });
        }
        public void SetData()
        {
            string urlData = $"{url}/User.json?auth={secret}";

            RestClient.Get(urlData).Then(response =>
            {
                Debug.Log(response.Text);
                RestClient.Put<User>(urlData, user).Then(response =>
                {
                    Debug.Log("Upload Data Complete");
                }).Catch(error =>
                {
                    Debug.Log("error on set to server");
                });




                //for( int i = 0; i < jsonNode.Count; i++)
                // {
                //    user.userData.Add(new User.UserData(jsonNode[i]["name"], jsonNode[i]["gender"], jsonNode[i]["age"]));
                // }
                // user = new User(jsonNode["name"],jsonNode["gender"],jsonNode["age"]);

            }).Catch(error =>
            {

                Debug.Log("error");
            });
        }
    }
}
