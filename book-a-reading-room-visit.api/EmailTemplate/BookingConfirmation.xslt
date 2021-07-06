<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <body style="font-family: Arial, sans-serif;font-size: 16px;line-height: 24px;">
      <p>
        Dear <xsl:value-of select="Name" />,
      </p>
      <p>
        You have booked a visit to view original documents in The National Archives’ reading rooms on <xsl:value-of select="VisitStartDate" />.
      </p>
      <p>
        Your document order has been submitted and we will begin preparing your documents for your visit.
      </p>
      <p>
        We will be in contact if we have any questions about your visit.
      </p>
      <h3 style="margin-top: 2em;">Your booking summary </h3>
		<p>
            Name: <xsl:value-of select="Name" /><br/>
            Telephone number is: <xsl:value-of select="Phone" /><br/>
            Your booking reference is: <xsl:value-of select="BookingReference" /><br/>
            Your Reader’s ticket number is: <xsl:value-of select="ReaderTicket" /><br/>
            Access needs and research requests:<xsl:value-of select="AdditionalRequirements" />
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
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">
              <xsl:if test="VisitEndDate">Visit start date</xsl:if>
              <xsl:if test="not(VisitEndDate)">Visit date</xsl:if>
            </th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="VisitStartDate" />
            </td>
          </tr>
          <xsl:if test="VisitEndDate">
            <tr>
              <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Visit end date</th>
              <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
                <xsl:value-of select="VisitEndDate" />
              </td>
            </tr>
          </xsl:if>
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
              <br />
              Based on availability, we may need to change your seat on the date of your visit.
            </td>
          </tr>
        </tbody>
      </table>

      <h3 style="margin-top: 2em;">Your document order</h3>
      
      <xsl:for-each select="DocumentOrder">
        <p>
          <b>
            <xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Reference" /> : <xsl:value-of select="Description" />
        </p>
      </xsl:for-each>

      <xsl:for-each select="ReserveDocumentOrder">
        <p>
          <b>
            <xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Reference" /> : <xsl:value-of select="Description" />
        </p>
      </xsl:for-each>

      <hr />

      <h3 style="margin-top: 2em;">Prepare for your visit</h3>
	  <p>
		  We have had to amend our reading room rules in line with Covid restrictions, please check what is now permissible to bring into the
	      reading rooms: <a href="https://www.nationalarchives.gov.uk/about/visit-us/researching-here/can-take-reading-rooms/">https://www.nationalarchives.gov.uk/about/visit-us/researching-here/can-take-reading-rooms/</a>
	  </p>
	  <p>
		  Bring your reader’s ticket. If you have a temporary ticket number, remember to bring the correct documents to complete your
		  registration. See a list of acceptable forms of identification: <a href="https://www.nationalarchives.gov.uk/about/visit-us/researching-here/do-i-need-a-readers-ticket/">https://www.nationalarchives.gov.uk/about/visit-us/researching-here/do-i-need-a-readers-ticket/</a>
	  </p>
	  <p style="font-weight: bold;">
		  We regularly review our opening times and reading room set-up in line with latest government advice. Check these links before your visit:
	  </p>
	  <ul style="list-style: none;">
		  <li>
			  See our opening times: <a href="https://www.nationalarchives.gov.uk/about/visit-us/opening-times/ ">https://www.nationalarchives.gov.uk/about/visit-us/opening-times/</a>
		  </li>
		  <li>
			  What to expect when you visit:<a href="https://www.nationalarchives.gov.uk/about/visit-us/researching-here/what-can-i-expect-when-i-visit/">https://www.nationalarchives.gov.uk/about/visit-us/researching-here/what-can-i-expect-when-i-visit/</a>
		  </li>
	  </ul>
		
      <h3 style="margin-top: 2em;">Cancel your visit</h3>
	  <p>
		  You can cancel your visit at any time. Use this link: <xsl:value-of select="ReturnURL" />
	  </p>
		
      <h3 style="margin-top: 2em;">Complete our survey</h3>
	  <p>
		  This is a redesigned service. Complete our survey so we can keep improving this service:
		  <a href="https://www.smartsurvey.co.uk/s/NewVisitBooking/">https://www.smartsurvey.co.uk/s/NewVisitBooking/</a>
      </p>

	  <p>
		  This email inbox is not being monitored.
	  </p>
	</body>
  </xsl:template>
</xsl:stylesheet>
