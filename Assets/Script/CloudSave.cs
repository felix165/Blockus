using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using System;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using TMPro;


public class CloudSave : MonoBehaviour
{
    [HideInInspector]
    public static bool isSignIn = false;

    async void Start()
    {
        //UnityServices.Initialize() will initialize all services that are subscribed to Core
        await UnityServices.InitializeAsync();
        Debug.Log($"Unity services initialization: {UnityServices.State}");

        //Shows if a cached session token exist
        Debug.Log($"Cached Session Token Exist: {AuthenticationService.Instance.SessionTokenExists}");

        // Shows Current profile
        Debug.Log(AuthenticationService.Instance.Profile);

        AuthenticationService.Instance.SignedIn += () =>
        {
            //Shows how to get a playerID
            Debug.Log($"PlayedID: {AuthenticationService.Instance.PlayerId}");

            //Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");
            Debug.Log($"Profile: {AuthenticationService.Instance.Profile}");

            Debug.Log("Sign in anonymously succeeded!");
            isSignIn= true;
        };

        AuthenticationService.Instance.SignedOut += () =>
        {
            Debug.Log("Signed Out!");
            isSignIn= false;
        };
        //You can listen to events to display custom messages
        AuthenticationService.Instance.SignInFailed += errorResponse =>
        {
            Debug.LogError($"Sign in anonymously failed with error code: {errorResponse.ErrorCode}");
        };
    }

    public async void OnClickSignIn()
    {
        try
        {
            if (GameManager.profileName == "")
            {
                OnClearSessionToken();
            }
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"Sign in anonymously failed with error code: {ex.ErrorCode}");
        }

    }

    public void OnClearSessionToken()
    {
        AuthenticationService.Instance.ClearSessionToken();
        Debug.Log("Token cleared");
    }


    public void OnClickSwitchProfile()
    {
        try
        {
            if (GameManager.profileName != "")
            {
                AuthenticationService.Instance.SwitchProfile(GameManager.profileName);
            }
            else
            {
                AuthenticationService.Instance.SwitchProfile("default");
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            GameManager.profileName = AuthenticationService.Instance.Profile;
        }
        Debug.Log($"Current Profile: {AuthenticationService.Instance.Profile}");
    }

    public async void OnClickSignOut()
    {
        AuthenticationService.Instance.SignOut();
    }


    #region cloudSave
    //List ALL Key
    public async Task<List<string>> ListAllKeys<T>()
    {
        try
        {
            var keys = await CloudSaveService.Instance.Data.RetrieveAllKeysAsync();

            Debug.Log($"Keys count: {keys.Count}\n" + $"Keys: {String.Join(", ", keys)}");
            return keys;
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
        return default;
    }

    public async Task<int> KeysCount()
    {
        try
        {
            var keys = await CloudSaveService.Instance.Data.RetrieveAllKeysAsync();

            Debug.Log($"Keys count: {keys.Count}\n" + $"Keys: {String.Join(", ", keys)}");
            return keys.Count;
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
        return default;
    }

    //Push single Data
    public async Task ForceSaveSingleData(string key, string value)
    {
        try
        {
            Dictionary<string, object> oneElement = new Dictionary<string, object>();

            // It's a text input field, but let's see if you actually entered a number.
            if (Int32.TryParse(value, out int wholeNumber))
            {
                oneElement.Add(key, wholeNumber);
            }
            else if (Single.TryParse(value, out float fractionalNumber))
            {
                oneElement.Add(key, fractionalNumber);
            }
            else
            {
                oneElement.Add(key, value);
            }

            await CloudSaveService.Instance.Data.ForceSaveAsync(oneElement);

            Debug.Log($"Successfully saved {key}:{value}");
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
    }

    //Save Pbject Data
    public async Task ForceSaveObjectData(string key, object value)
    {
        try
        {
            // Although we are only saving a single value here, you can save multiple keys
            // and values in a single batch.
            Dictionary<string, object> oneElement = new Dictionary<string, object>
                {
                    { key, value }
                };

            await CloudSaveService.Instance.Data.ForceSaveAsync(oneElement);

            Debug.Log($"Successfully saved {key}:{value}");
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
    }

    //Read Spesific Data
    public async Task<T> RetrieveSpecificData<T>(string key)
    {
        try
        {
            var results = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { key });

            if (results.TryGetValue(key, out string value))
            {
                return JsonUtility.FromJson<T>(value);
            }
            else
            {
                Debug.Log($"There is no such key as {key}!");
            }
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }

        return default;
    }

    //ReadAll
    public async Task RetrieveEverything()
    {
        try
        {
            // If you wish to load only a subset of keys rather than everything, you
            // can call a method LoadAsync and pass a HashSet of keys into it.
            var results = await CloudSaveService.Instance.Data.LoadAllAsync();

            Debug.Log($"Elements loaded!");

            foreach (var element in results)
            {
                Debug.Log($"Key: {element.Key}, Value: {element.Value}");
            }
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
    }

    //Delete
    public async Task ForceDeleteSpecificData(string key)
    {
        try
        {
            await CloudSaveService.Instance.Data.ForceDeleteAsync(key);

            Debug.Log($"Successfully deleted {key}");
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
    }
    #endregion









}
