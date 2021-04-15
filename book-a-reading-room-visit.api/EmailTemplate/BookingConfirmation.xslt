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
              <br />
              <span>Based on availability, we may need to change your seat on the date of your visit.</span>
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
          <xsl:value-of select="Document" />
        </p>
      </xsl:for-each>

      <xsl:for-each select="ReserveDocumentOrder">
        <p>
          <b>
            <xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Document" />
        </p>
      </xsl:for-each>

      <hr />

      <h3 style="margin-top: 2em;">Prepare for your visit</h3>
      <p>Find out about:</p>
      <ul>
        <li>
          how to get here: 
		  <a href="https://www.nationalarchives.gov.uk/about/visit-us/how-to-find-us/">
			  https://www.nationalarchives.gov.uk/about/visit-us/how-to-find-us/
		  </a>
        </li>
        <li>
          what you can take into the reading rooms: 
		   <a href="https://www.nationalarchives.gov.uk/about/visit-us/researching-here/can-take-reading-rooms/">
			   https://www.nationalarchives.gov.uk/about/visit-us/researching-here/can-take-reading-rooms/
		   </a>
        </li>
        <li>
          how to use the reading rooms (rules for readers): 
		  <a href="https://www.nationalarchives.gov.uk/documents/rules.pdf">
			  https://www.nationalarchives.gov.uk/documents/rules.pdf
		  </a>
        </li>
        <li>
          more information about this service: 
		    <a href="https://www.nationalarchives.gov.uk/about/visit-us/about-the-book-a-reading-room-visit-service/">
				https://www.nationalarchives.gov.uk/about/visit-us/about-the-book-a-reading-room-visit-service/
			</a>
        </li>
      </ul>
      <p>
        Remember to bring the correct documents to complete your registration if you are using your reader's ticket for the first time.
      </p>
      <p>
        Bring your device if you wish to take photographs of our documents. We do not charge for this.
      </p>
      <h3 style="margin-top: 2em;">What to expect when you visit</h3>
      <p>
        Watch this video to find out what you can expect during your booked visit to The National Archives' reading rooms and the changes we've made for everyone's safety:
        <a href="https://www.youtube.com/watch?v=Wm4mq5X_EFs">
			https://www.youtube.com/watch?v=Wm4mq5X_EFs
		</a>
      </p>
      <p>Our building is COVID-19 secure. You have agreed to comply with our Coronavirus Visitor Charter as part of the booking process:
	    <a href="https://www.nationalarchives.gov.uk/documents/coronavirus-visitor-charter.pdf">
			https://www.nationalarchives.gov.uk/documents/coronavirus-visitor-charter.pdf
		</a>
	  </p>
      <p>Remember to wear a face mask when you visit, unless you have a valid exemption:
	    <a href="https://www.gov.uk/government/publications/face-coverings-when-to-wear-one-and-how-to-make-your-own/face-coverings-when-to-wear-one-and-how-to-make-your-own#exemptions">
			https://www.gov.uk/government/publications/face-coverings-when-to-wear-one-and-how-to-make-your-own/face-coverings-when-to-wear-one-and-how-to-make-your-own#exemptions
		</a>
	  </p>
      <h3 style="margin-top: 2em;">Cancel your visit</h3>
	  <p>
          You can cancel your visit at any time. Use this link: <xsl:value-of select="ReturnURL" />
	  </p>
      <h3 style="margin-top: 2em;">Need help?</h3>

	  <p>
        This email inbox is not being monitored. Contact us if you need help with this service: 
	    <a href="https://www.nationalarchives.gov.uk/contact-us/">
			https://www.nationalarchives.gov.uk/contact-us/
		</a>
	  </p>
	  
      <h3 style="margin-top: 2em;">Complete our survey</h3>
	  <p>
        This is a redesigned service. Complete our survey so we can keep improving this service:
		<a href="https://www.smartsurvey.co.uk/s/NewVisitBooking/">
			https://www.smartsurvey.co.uk/s/NewVisitBooking/
		</a>
      </p>
	</body>
  </xsl:template>
</xsl:stylesheet>
