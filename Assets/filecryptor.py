
mode = input("mode: ")[0] == 'd'


def encrypt(src, dest):
    for line in src:
        if line == "?\n":
            dest.write(bytes([0x60]))
        elif line == ":\n":
            dest.write(bytes([0x70]))
        else:
            for char in line:
                if char == "\n":
                    dest.write(bytes([0x50]))
                    break
                print (ord(char))
                dest.write(bytes([ord(char) - 0x30]))
                #fwrite.write()
            

def decrypt(source, dest):
    src = source.read()
    for byte in src:
        print (byte)
        if byte == 0x50:
            dest.write("\n")
        elif byte == 0x60:
            dest.write("?\n")
        elif byte == 0x70:
            dest.write(":\n")
        else:
            dest.write(chr(byte + 0x30))
                #fwrite.write()
            #dest.write(chr(0x50))

if mode:
    fread = open(input("read: ")+".bin", "rb")
    fwrite = open(input("write: ")+".txt","w")
    decrypt(fread, fwrite)
    fread.close()
    fwrite.close()
else:
    fread = open(input("read: ")+".txt", "r")
    fwrite = open(input("write: ")+".bin","wb")
    encrypt(fread, fwrite)
    fread.close()
    fwrite.close()

        
        
    
