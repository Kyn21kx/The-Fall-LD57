public static class Assert {

	public static void NotNull<T>(T? obj, string message = "") {
		if (obj != null) return;
		throw new System.Exception($"Assertion failed! {message}");
	}

	public static void IsTrue(bool condition, string message = "") {
		if (!condition) {
			throw new System.Exception($"Assertion failed! {message}");
		}
	}
	
}

