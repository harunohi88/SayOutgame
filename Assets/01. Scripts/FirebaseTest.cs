using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Firestore;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseFirestore _db;
    
    
    private void Start()
    {
        Init();
    }
    
    private void Init()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                _app = Firebase.FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;

                Login();

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            } else {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private void Register()
    {
        string email = "test@gmail.com";
        string password = "111111";
        
        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted) {
                Debug.LogError($"회원가입에 실패했습니다: {task.Exception.Message}");
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("회원가입에 성공했습니다: {0} ({1})", result.User.DisplayName, result.User.UserId);
            return;
        });
    }

    private void Login()
    {
        string email = "test@gmail.com";
        string password = "111111";
        
        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted) {
                Debug.LogError($"로그인에 실패했습니다: {task.Exception.Message}");
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("로그인에 성공했습니다: {0} ({1})", result.User.DisplayName, result.User.UserId);
        });
        
        NicknameChange();
        // AddMyRanking();
        GetRankings();
    }

    private void NicknameChange()
    {
        Firebase.Auth.FirebaseUser user = _auth.CurrentUser;

        if (user == null) return;
        
        Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile { 
            DisplayName = "Teemo",
        };
        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task => { 
            if (task.IsCanceled || task.IsFaulted) { 
                Debug.LogError("닉네임 변경에 실패했습니다: " + task.Exception); 
                return;
            }
            
            Debug.Log("닉네임 변경에 성공했습니다.");
        });
    }

    private void GetProfile()
    {
        Firebase.Auth.FirebaseUser user = _auth.CurrentUser;
        if (user != null) {
            string name = user.DisplayName;
            string email = user.Email;
            System.Uri photo_url = user.PhotoUrl;
            // The user's Id, unique to the Firebase project.
            // Do NOT use this value to authenticate with your backend server, if you
            // have one; use User.TokenAsync() instead.
            string uid = user.UserId;

            Account account = new Account(email, name, "firebase");
        }
    }

    private void AddMyRanking()
    {
        Ranking ranking = new Ranking("eoheo@gmail.com", "어허", 4000);
        
        Dictionary<string, object> rankingDict = new Dictionary<string, object>
        {
            { "Email", ranking.Email },
            { "Nickname", ranking.Nickname },
            { "Score", ranking.KillCount }
        };
        _db.Collection("rankings").Document(ranking.Email).SetAsync(rankingDict).ContinueWithOnMainThread(task => {
            Debug.Log(String.Format("Added document with ID: {0}.", ranking.Email));
        });
    }

    private void GetMyRanking()
    {
        Ranking ranking = new Ranking("eoheo@gmail.com", "어허", 4000);
        
        DocumentReference docRef = _db.Collection("rankings").Document(ranking.Email);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> rankings = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in rankings)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }

    private void GetRankings()
    {
        Query allRankingsQuery = _db.Collection("rankings");
        allRankingsQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                Dictionary<string, object> city = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }

                // Newline to separate entries
                Debug.Log("");
            }
        });
    }
}
