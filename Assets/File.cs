using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DirectoryFile : FileStream {
	public DirectoryFile(string name, string mode) : base(name, getModeFromString(mode)){
	}
	public static FileMode getModeFromString(string mode){
		if (Equals (mode, "Read") || Equals (mode, "Open"))
			return FileMode.Open;
		throw new IOException("mode unavalible");
	}
	public DirectoryFile Make(string name, string mode){
		return new DirectoryFile(name, mode);
	}
}
