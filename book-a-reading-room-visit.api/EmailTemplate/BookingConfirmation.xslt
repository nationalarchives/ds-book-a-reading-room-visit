<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <body style="font-family:Arial;font-size:9pt">
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
      <h3>Your booking summary </h3>
      Name: <xsl:value-of select="Name" /><br/>
      <xsl:if test="Phone">
      Telephone number is: <xsl:value-of select="Phone" /><br/>
      </xsl:if>
      Your booking reference is: <xsl:value-of select="BookingReference" /><br/>
      Your Reader’s ticket number is: <xsl:value-of select="ReaderTicket" /><br/>
      <xsl:if test="AdditionalRequirements">
      Access needs and research requests:<xsl:value-of select="AdditionalRequirements" /><br/>
      </xsl:if>
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
              <br />
              <span>Based on availability, we may need to change your seat on the date of your visit.</span>
            </td>
          </tr>
        </tbody>
      </table>

      <h3>Your document order</h3>

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

      <h3>Prepare for your visit</h3>
      <p>Find out about:</p>
      <ul>
        <li>
          how to get here: https://www.nationalarchives.gov.uk/about/visit-us/how-to-find-us/
        </li>
        <li>
          what you can take into the reading rooms: https://www.nationalarchives.gov.uk/about/visit-us/researching-here/can-take-reading-rooms/
        </li>
        <li>
          how to use the reading rooms (Rules for readers): https://www.nationalarchives.gov.uk/documents/rules.pdf
        </li>
        <li>
          what to expect when you visit: https://www.nationalarchives.gov.uk/about/visit-us/about-the-book-a-reading-room-visit-service/ and https://www.youtube.com/watch?v=Wm4mq5X_EFs (video)
        </li>
      </ul>
      <p>
        If you are using your reader's ticket for the first time remember to bring the correct documents to complete your registration.
      </p>
      <p>
        If you wish to take photographs of our documents please bring your device. We do not charge for this.
      </p>
      <h3>What to expect when you visit</h3>
      <p>Our building is COVID-19 secure. You have agreed to comply with our Coronavirus Visitor Charter as part of the booking process: https://www.nationalarchives.gov.uk/documents/coronavirus-visitor-charter.pdf</p>
      <p>Please remember to wear a face mask when you visit, unless you have a valid exemption: https://www.gov.uk/government/publications/face-coverings-when-to-wear-one-and-how-to-make-your-own/face-coverings-when-to-wear-one-and-how-to-make-your-own#exemptions</p>
      <h3>Cancel your visit</h3>
      You can cancel your visit at any time. Use this link: <xsl:value-of select="ReturnURL" />
      <h3>Need help?</h3>

      This email inbox is not being monitored. Contact us if you need help with this service: https://www.nationalarchives.gov.uk/contact-us/
      <h3>Complete our survey</h3>
      This is a redesigned service. Complete our survey so we can keep improving this service: https://www.smartsurvey.co.uk/s/NewVisitBooking/
    </body>
  </xsl:template>
</xsl:stylesheet>
