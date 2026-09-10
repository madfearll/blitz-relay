namespace BlitzRelay.Rooms;

internal sealed record PublicRoomSnapshot
(
	string Code,
	RoomKind Kind,
	int MaximumClients,
	int ConnectedClientCount,
	bool HasHost,
	Dictionary<string, string> Metadata
)
{
	private static readonly HashSet<string> PublicMetadataKeys = new(StringComparer.OrdinalIgnoreCase)
	{
		"HostName",
		"Version",
		"Users",
		"MaxMembers",
		"DisconnectedFriends",
	};

	public static PublicRoomSnapshot FromRoom(Room room)
	{
		Dictionary<string, string> metadata = new(StringComparer.OrdinalIgnoreCase);

		foreach ((string key, string value) in room.Metadata)
		{
			if (PublicMetadataKeys.Contains(key)) metadata[key] = value;
		}

		return new PublicRoomSnapshot
		(
			room.Code,
			room.Kind,
			room.MaximumClients,
			room.ClientsByVirtualId.Count,
			room.HasActiveHost,
			metadata
		);
	}
}