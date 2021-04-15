<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <body style="font-family: Arial, sans-serif;font-size: 16px;line-height: 24px;">
      <p>
        Dear <xsl:value-of select="Name" />,
      </p>
      <p>
        Your visit to The National Archives’ reading rooms has been cancelled.
      </p>
      <h3 style="margin-top: 2em;">The following booking was cancelled</h3>
		<p>
			Your booking reference is: <xsl:value-of select="BookingReference" /><br/>
            Your reader’s ticket number is: <xsl:value-of select="ReaderTicket" />
	    </p>
      <table style="text-align: left;border: 1px solid #999;border-collapse: collapse;margin: 2em 0;">
        <tbody>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Visit type</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="VisitType" />
            </td>
          </tr>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Visit date</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="VisitStartDate" />
            </td>
          </tr>
          <xsl:if test="string(ReadingRoom)">
            <tr>
              <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Reading room</th>
              <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
                <xsl:value-of select="ReadingRoom" />
              </td>
            </tr>
          </xsl:if>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Seat number</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="SeatNumber" />
            </td>
          </tr>
        </tbody>
      </table>

      <h3 style="margin-top: 2em;">What can I do next?</h3>
      <p>
        Book another visit to view our documents: <xsl:value-of select="HomeURL" />
      </p>
      <p>
        Find out more about this service: 
		  <a href="https://www.nationalarchives.gov.uk/about/visit-us/about-the-book-a-reading-room-visit-service/">
			  https://www.nationalarchives.gov.uk/about/visit-us/about-the-book-a-reading-room-visit-service/
		  </a>					 
      </p>

      <h3 style="margin-top: 2em;">Need help?</h3>

		<p>
			This email inbox is not being monitored. Contact us if you need help with this service: 
	        <a href="https://www.nationalarchives.gov.uk/contact-us/">
			    https://www.nationalarchives.gov.uk/contact-us/
		    </a>
		</p>
    </body>
  </xsl:template>
</xsl:stylesheet>
