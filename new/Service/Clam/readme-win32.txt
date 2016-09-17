ClamAV/SOSDG for 9x/NT/2k/XP/2003
Port maintained by Brielle Bruns <bruns@2mbit.com>
http://www.sosdg.org/clamav-win32


What is Clamav?
Clamav is an open source antivirus program.  While it may not have all of 
the features of the big business antivirus software (like Norton 
Antivirus), Clamav is fast and stable, and comes with none of the catches 
like paid subscriptions for virus database updates.


Why port it to Windows?
I ported this to Windows one afternoon because I felt that someone out 
there might be able  to use it.  I'm familiar with porting UNIX/Linux 
applications to Windows (I maintain ircII EPIC4 For Windows as well).  
This is not a 100% native port.  I used the Cygwin layer to make Clamav 
work correctly under Win32.


Does everything work correctly?
So far everything does - the initial tests show that clamd works, as well 
as the scanning  tools, reshclam, and sigtool.


Having problems with a missing TMP dir?

Make sure to set the env variable TMPDIR to a working temp directory that 
does exist.

set TMPDIR=/cygdrive/c/clamav-devel/tmp

Extra Credit
I borrowed from the Cygwin ClamAV package clamav-0.88.2-1.patch and stripped it of
unncessary content for my build.  This was done so I can successfully build the DLL
version of ClamAV again which requires a bit of...  beating to the configure/libtool
scripts.

Thanks to Reini Urban <rurban@x-ray.at> for his work in that aspect.  I've included
both the original patch and the new patch created by me in the SOSDG folder.