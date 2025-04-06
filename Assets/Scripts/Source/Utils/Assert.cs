using Hazel;

public static class Assert {

	public static void NotNull<T>(T? obj, string message = "") {
		if (obj != null) return;
		string errMsg = $"Assertion failed! {message}"; 
		Log.Error(errMsg);
		throw new System.Exception(errMsg);
	}

	public static void IsTrue(bool condition, string message = "") {
		if (!condition) {
			string errMsg = $"Assertion failed! {message}"; 
			Log.Error(errMsg);
			throw new System.Exception(errMsg);
		}
	}
	
}

