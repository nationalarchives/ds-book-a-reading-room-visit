<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <body style="font-family:Arial;font-size:9pt">
      <p>
        Dear <xsl:value-of select="Name" />,
      </p>
      <p>
        Your visit to The National Archives’ reading rooms on <xsl:value-of select="VisitStartDate" /> has been cancelled.
      </p>
      <h3>The following booking was cancelled</h3>
      Your booking reference is: <xsl:value-of select="BookingReference" /><br/>
      Your Reader’s ticket number is: <xsl:value-of select="ReaderTicket" /><br/>
      <table>
        <tbody>
          <tr>
            <th>Visit type</th>
            <td>
              <xsl:value-of select="VisitType" />
            </td>
          </tr>
          <tr>
            <th>Visit date</th>
            <td>
              <xsl:value-of select="VisitStartDate" />
            </td>
          </tr>
          <tr>
            <th>Reading room</th>
            <td>
              <xsl:value-of select="ReadingRoom" />
            </td>
          </tr>
          <tr>
            <th>Seat number</th>
            <td>
              <xsl:value-of select="SeatNumber" />
            </td>
          </tr>
        </tbody>
      </table>

      <h3>What can I do next?</h3>
      <p>
        Book another visit to view our documents: <xsl:value-of select="HomeURL" />
      </p>
      <p>
        Find out more about this service: https://www.nationalarchives.gov.uk/about/visit-us/about-the-book-a-reading-room-visit-service/
      </p>

      <h3>Need help?</h3>

      This email inbox is not being monitored. Contact us if you need help with this service: https://www.nationalarchives.gov.uk/contact-us/
    </body>
  </xsl:template>
</xsl:stylesheet>
