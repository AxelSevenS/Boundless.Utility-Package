namespace Seven.Boundless.Utility;

using Godot;

public static class Vector2Extensions {
	public static Vector2 SafeSlerp(this Vector2 from, Vector2 to, float weight) {
		if (from.IsEqualApprox(to)) {
			return from;
		}

		// Avoid error on both vectors being inverses of each other, breaking a Cross Product operation in the Slerp method

		if ((from + to) == Vector2.Zero) {
			return from.Lerp(to, weight);
		}

		return from.Slerp(to, weight);
	}

	public static Vector2 ClampMagnitude(this Vector2 vector2, float maxLength) {
		if (vector2.LengthSquared() > maxLength * maxLength) {
			return vector2.Normalized() * maxLength;
		}
		return vector2;
	}

	public static Vector2 ClampedLerp(this Vector2 from, Vector2 to, float amount) {
		Vector2 res = from + (to - from) * amount;
		if (amount > Mathf.Epsilon && from.IsEqualApprox(res)) {
			return to;
		}
		return res;
	}


	/// <summary>
	/// Returns the target vector after being "flattened" against a surface defined by the given normal.
	/// </summary>
	/// <param name="vector"></param>
	/// <param name="direction"></param>
	/// <returns></returns>
	public static Vector2 SlideOnFace(this Vector2 vector, Vector2 normal) =>
		vector + normal * Mathf.Max(vector.Project(-normal).Dot(-normal), 0);

	public static Vector2 To(this Vector2 vector, Vector2 to)
		=> new(to.X - vector.X, to.Y - vector.Y);
}