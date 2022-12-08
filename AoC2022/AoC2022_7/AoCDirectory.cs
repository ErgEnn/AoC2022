using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022_7
{
    public class AoCDirectory
    {
        public string Name { get; set; }
        public AoCDirectory Parent { get; set; }
        public Dictionary<string,AoCDirectory> Children { get; set; } = new Dictionary<string, AoCDirectory>();
        public Dictionary<string,AoCFile> Files { get; set; } = new Dictionary<string, AoCFile>();

        public AoCDirectory(string name, AoCDirectory parent)
        {
            Name = name;
            Parent = parent;
        }

        public AoCDirectory GetOrCreateChild(string name)
        {
            if (Children.TryGetValue(name, out var directory))
            {
                return directory;
            }
            directory = new AoCDirectory(name, this);
            Children.Add(name, directory);
            return directory;
        }

        public void GetOrCreateFile(string name, string size)
        {
            if (Files.TryGetValue(name, out var file))
            {
                return;
            }
            file = new AoCFile { Name = name, Size = int.Parse(size) };
            Files.Add(name, file);
            return;
        }

        public int GetSize()
        {
            return Children.Sum(child => child.Value.GetSize()) + Files.Sum(file => file.Value.Size);
        }
    }

    public class AoCFile
    {
        public string Name { get; set; }
        public int Size { get; set; }
    }
}
