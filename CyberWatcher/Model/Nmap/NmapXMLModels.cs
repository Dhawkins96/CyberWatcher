using System.Collections.Generic;
using System.Xml.Serialization;

namespace CyberWatcher.Model.Nmap
{

    [XmlRoot(ElementName = "scaninfo")]
    public class Scaninfo
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "protocol")]
        public string Protocol { get; set; }
        [XmlAttribute(AttributeName = "numservices")]
        public string Numservices { get; set; }
        [XmlAttribute(AttributeName = "services")]
        public string Services { get; set; }
    }

    [XmlRoot(ElementName = "verbose")]
    public class Verbose
    {
        [XmlAttribute(AttributeName = "level")]
        public string Level { get; set; }
    }

    [XmlRoot(ElementName = "debugging")]
    public class Debugging
    {
        [XmlAttribute(AttributeName = "level")]
        public string Level { get; set; }
    }

    [XmlRoot(ElementName = "taskbegin")]
    public class Taskbegin
    {
        [XmlAttribute(AttributeName = "task")]
        public string Task { get; set; }
        [XmlAttribute(AttributeName = "time")]
        public string Time { get; set; }
    }

    [XmlRoot(ElementName = "taskend")]
    public class Taskend
    {
        [XmlAttribute(AttributeName = "task")]
        public string Task { get; set; }
        [XmlAttribute(AttributeName = "time")]
        public string Time { get; set; }
        [XmlAttribute(AttributeName = "extrainfo")]
        public string Extrainfo { get; set; }
    }

    [XmlRoot(ElementName = "status")]
    public class Status
    {
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "reason")]
        public string Reason { get; set; }
        [XmlAttribute(AttributeName = "reason_ttl")]
        public string Reason_ttl { get; set; }
    }

    [XmlRoot(ElementName = "address")]
    public class Address
    {
        [XmlAttribute(AttributeName = "addr")]
        public string Addr { get; set; }
        [XmlAttribute(AttributeName = "addrtype")]
        public string Addrtype { get; set; }
        [XmlAttribute(AttributeName = "vendor")]
        public string Vendor { get; set; }
    }

    [XmlRoot(ElementName = "hosthint")]
    public class Hosthint
    {
        [XmlElement(ElementName = "status")]
        public Status Status { get; set; }
        [XmlElement(ElementName = "address")]
        public List<Address> Address { get; set; }
        [XmlElement(ElementName = "hostnames")]
        public string Hostnames { get; set; }
    }

    [XmlRoot(ElementName = "host")]
    public class Host
    {
        [XmlElement(ElementName = "status")]
        public Status Status { get; set; }
        [XmlElement(ElementName = "address")]
        public List<Address> Address { get; set; }
        [XmlElement(ElementName = "hostnames")]
        public string Hostnames { get; set; }
        [XmlElement(ElementName = "ports")]
        public Ports Ports { get; set; }
        [XmlElement(ElementName = "os")]
        public Os Os { get; set; }
        [XmlElement(ElementName = "distance")]
        public Distance Distance { get; set; }
        [XmlElement(ElementName = "trace")]
        public Trace Trace { get; set; }
        [XmlElement(ElementName = "times")]
        public Times Times { get; set; }
        [XmlAttribute(AttributeName = "starttime")]
        public string Starttime { get; set; }
        [XmlAttribute(AttributeName = "endtime")]
        public string Endtime { get; set; }
        [XmlElement(ElementName = "uptime")]
        public Uptime Uptime { get; set; }
        [XmlElement(ElementName = "tcpsequence")]
        public Tcpsequence Tcpsequence { get; set; }
        [XmlElement(ElementName = "ipidsequence")]
        public Ipidsequence Ipidsequence { get; set; }
        [XmlElement(ElementName = "tcptssequence")]
        public Tcptssequence Tcptssequence { get; set; }
        [XmlElement(ElementName = "hostscript")]
        public Hostscript Hostscript { get; set; }
    }

    [XmlRoot(ElementName = "taskprogress")]
    public class Taskprogress
    {
        [XmlAttribute(AttributeName = "task")]
        public string Task { get; set; }
        [XmlAttribute(AttributeName = "time")]
        public string Time { get; set; }
        [XmlAttribute(AttributeName = "percent")]
        public string Percent { get; set; }
        [XmlAttribute(AttributeName = "remaining")]
        public string Remaining { get; set; }
        [XmlAttribute(AttributeName = "etc")]
        public string Etc { get; set; }
    }

    [XmlRoot(ElementName = "extrareasons")]
    public class Extrareasons
    {
        [XmlAttribute(AttributeName = "reason")]
        public string Reason { get; set; }
        [XmlAttribute(AttributeName = "count")]
        public string Count { get; set; }
        [XmlAttribute(AttributeName = "proto")]
        public string Proto { get; set; }
        [XmlAttribute(AttributeName = "ports")]
        public string Ports { get; set; }
    }

    [XmlRoot(ElementName = "extraports")]
    public class Extraports
    {
        [XmlElement(ElementName = "extrareasons")]
        public Extrareasons Extrareasons { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "count")]
        public string Count { get; set; }
    }

    [XmlRoot(ElementName = "ports")]
    public class Ports
    {
        [XmlElement(ElementName = "extraports")]
        public Extraports Extraports { get; set; }
        [XmlElement(ElementName = "port")]
        public List<Port> Port { get; set; }
    }

    [XmlRoot(ElementName = "portused")]
    public class Portused
    {
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "proto")]
        public string Proto { get; set; }
        [XmlAttribute(AttributeName = "portid")]
        public string Portid { get; set; }
    }

    [XmlRoot(ElementName = "osfingerprint")]
    public class Osfingerprint
    {
        [XmlAttribute(AttributeName = "fingerprint")]
        public string Fingerprint { get; set; }
    }

    [XmlRoot(ElementName = "os")]
    public class Os
    {
        [XmlElement(ElementName = "portused")]
        public List<Portused> Portused { get; set; }
        [XmlElement(ElementName = "osfingerprint")]
        public Osfingerprint Osfingerprint { get; set; }
        [XmlElement(ElementName = "osmatch")]
        public List<Osmatch> Osmatch { get; set; }
    }

    [XmlRoot(ElementName = "distance")]
    public class Distance
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "hop")]
    public class Hop
    {
        [XmlAttribute(AttributeName = "ttl")]
        public string Ttl { get; set; }
        [XmlAttribute(AttributeName = "ipaddr")]
        public string Ipaddr { get; set; }
        [XmlAttribute(AttributeName = "rtt")]
        public string Rtt { get; set; }
    }

    [XmlRoot(ElementName = "trace")]
    public class Trace
    {
        [XmlElement(ElementName = "hop")]
        public Hop Hop { get; set; }
    }

    [XmlRoot(ElementName = "times")]
    public class Times
    {
        [XmlAttribute(AttributeName = "srtt")]
        public string Srtt { get; set; }
        [XmlAttribute(AttributeName = "rttvar")]
        public string Rttvar { get; set; }
        [XmlAttribute(AttributeName = "to")]
        public string To { get; set; }
    }

    [XmlRoot(ElementName = "state")]
    public class State
    {
        [XmlAttribute(AttributeName = "state")]
        public string _state { get; set; }
        [XmlAttribute(AttributeName = "reason")]
        public string Reason { get; set; }
        [XmlAttribute(AttributeName = "reason_ttl")]
        public string Reason_ttl { get; set; }
    }

    [XmlRoot(ElementName = "service")]
    public class Service
    {
        [XmlElement(ElementName = "cpe")]
        public string Cpe { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "product")]
        public string Product { get; set; }
        [XmlAttribute(AttributeName = "method")]
        public string Method { get; set; }
        [XmlAttribute(AttributeName = "conf")]
        public string Conf { get; set; }
        [XmlAttribute(AttributeName = "servicefp")]
        public string Servicefp { get; set; }
        [XmlAttribute(AttributeName = "ostype")]
        public string Ostype { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "extrainfo")]
        public string Extrainfo { get; set; }
    }

    [XmlRoot(ElementName = "port")]
    public class Port
    {
        [XmlElement(ElementName = "state")]
        public State State { get; set; }
        [XmlElement(ElementName = "service")]
        public Service Service { get; set; }
        [XmlAttribute(AttributeName = "protocol")]
        public string Protocol { get; set; }
        [XmlAttribute(AttributeName = "portid")]
        public string Portid { get; set; }
        [XmlElement(ElementName = "script")]
        public List<Script> Script { get; set; }
    }

    [XmlRoot(ElementName = "elem")]
    public class Elem
    {
        [XmlAttribute(AttributeName = "key")]
        public string Key { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "script")]
    public class Script
    {
        [XmlElement(ElementName = "elem")]
        public List<Elem> Elem { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "output")]
        public string Output { get; set; }
        [XmlElement(ElementName = "table")]
        public Table Table { get; set; }
    }

    [XmlRoot(ElementName = "osclass")]
    public class Osclass
    {
        [XmlElement(ElementName = "cpe")]
        public List<string> Cpe { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "vendor")]
        public string Vendor { get; set; }
        [XmlAttribute(AttributeName = "osfamily")]
        public string Osfamily { get; set; }
        [XmlAttribute(AttributeName = "osgen")]
        public string Osgen { get; set; }
        [XmlAttribute(AttributeName = "accuracy")]
        public string Accuracy { get; set; }
    }

    [XmlRoot(ElementName = "osmatch")]
    public class Osmatch
    {
        [XmlElement(ElementName = "osclass")]
        public List<Osclass> Osclass { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "accuracy")]
        public string Accuracy { get; set; }
        [XmlAttribute(AttributeName = "line")]
        public string Line { get; set; }
    }

    [XmlRoot(ElementName = "uptime")]
    public class Uptime
    {
        [XmlAttribute(AttributeName = "seconds")]
        public string Seconds { get; set; }
        [XmlAttribute(AttributeName = "lastboot")]
        public string Lastboot { get; set; }
    }

    [XmlRoot(ElementName = "tcpsequence")]
    public class Tcpsequence
    {
        [XmlAttribute(AttributeName = "index")]
        public string Index { get; set; }
        [XmlAttribute(AttributeName = "difficulty")]
        public string Difficulty { get; set; }
        [XmlAttribute(AttributeName = "values")]
        public string Values { get; set; }
    }

    [XmlRoot(ElementName = "ipidsequence")]
    public class Ipidsequence
    {
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "values")]
        public string Values { get; set; }
    }

    [XmlRoot(ElementName = "tcptssequence")]
    public class Tcptssequence
    {
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "values")]
        public string Values { get; set; }
    }

    [XmlRoot(ElementName = "table")]
    public class Table
    {
        [XmlElement(ElementName = "elem")]
        public string Elem { get; set; }
        [XmlAttribute(AttributeName = "key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "hostscript")]
    public class Hostscript
    {
        [XmlElement(ElementName = "script")]
        public List<Script> Script { get; set; }
    }

    [XmlRoot(ElementName = "finished")]
    public class Finished
    {
        [XmlAttribute(AttributeName = "time")]
        public string Time { get; set; }
        [XmlAttribute(AttributeName = "timestr")]
        public string Timestr { get; set; }
        [XmlAttribute(AttributeName = "summary")]
        public string Summary { get; set; }
        [XmlAttribute(AttributeName = "elapsed")]
        public string Elapsed { get; set; }
        [XmlAttribute(AttributeName = "exit")]
        public string Exit { get; set; }
    }

    [XmlRoot(ElementName = "hosts")]
    public class Hosts
    {
        [XmlAttribute(AttributeName = "up")]
        public string Up { get; set; }
        [XmlAttribute(AttributeName = "down")]
        public string Down { get; set; }
        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }
    }

    [XmlRoot(ElementName = "runstats")]
    public class Runstats
    {
        [XmlElement(ElementName = "finished")]
        public Finished Finished { get; set; }
        [XmlElement(ElementName = "hosts")]
        public Hosts Hosts { get; set; }
    }

    [XmlRoot(ElementName = "nmaprun")]
    public class Nmaprun
    {
        [XmlElement(ElementName = "scaninfo")]
        public Scaninfo Scaninfo { get; set; }
        [XmlElement(ElementName = "verbose")]
        public Verbose Verbose { get; set; }
        [XmlElement(ElementName = "debugging")]
        public Debugging Debugging { get; set; }
        [XmlElement(ElementName = "taskbegin")]
        public List<Taskbegin> Taskbegin { get; set; }
        [XmlElement(ElementName = "taskend")]
        public List<Taskend> Taskend { get; set; }
        [XmlElement(ElementName = "hosthint")]
        public List<Hosthint> Hosthint { get; set; }
        [XmlElement(ElementName = "host")]
        public List<Host> Host { get; set; }
        [XmlElement(ElementName = "taskprogress")]
        public List<Taskprogress> Taskprogress { get; set; }
        [XmlElement(ElementName = "runstats")]
        public Runstats Runstats { get; set; }
        [XmlAttribute(AttributeName = "scanner")]
        public string Scanner { get; set; }
        [XmlAttribute(AttributeName = "args")]
        public string Args { get; set; }
        [XmlAttribute(AttributeName = "start")]
        public string Start { get; set; }
        [XmlAttribute(AttributeName = "startstr")]
        public string Startstr { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "xmloutputversion")]
        public string Xmloutputversion { get; set; }
    }


}