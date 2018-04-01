using UnityEngine;
namespace ca.HenrySoftware.Rage
{
	public static class Prefs
	{
		const string _zoomKey = "zoom";
		public static int Zoom
		{
			get { return PlayerPrefs.GetInt(_zoomKey, 2); }
			set
			{
				PlayerPrefs.SetInt(_zoomKey, value);
				PlayerPrefs.Save();
			}
		}
		const string _stateKey = "state";
		public static string State
		{
			get { return PlayerPrefs.GetString(_stateKey, null); }
			set
			{
				PlayerPrefs.SetString(_stateKey, value);
				PlayerPrefs.Save();
			}
		}
	}
}
