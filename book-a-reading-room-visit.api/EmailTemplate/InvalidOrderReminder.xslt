<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <body style="font-family:Arial;font-size:9pt">
      <p>
        Dear <xsl:value-of select="Name" />,
      </p>
      <p>
        You have a reservation to visit The National Archives’ reading rooms. This is a polite reminder that you need to complete your document order in order to confirm your visit.
      </p>
      <p>
        Make sure you complete the document order form by <b><xsl:value-of select="CompleteByDate" /> British Summer Time (BST).</b> If you do not complete the document order form by this deadline your visit will be automatically cancelled.
      </p>

      <p>
        Complete my document order <xsl:value-of select="ReturnURL" />.
        You will need your booking reference and reader's ticket number to complete this step.
      </p>

      <h3>Your booking summary</h3>
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
      <p>Use Discovery, our online catalogue, to gather your booking references: http://discovery.nationalarchives.gov.uk/ </p>

      <h3>Cancel your visit</h3>
      You can cancel your visit at any time. Use this link: <xsl:value-of select="ReturnURL" />

      <h3>Need help?</h3>

      This email inbox is not being monitored. Contact us if you need help with this service: https://www.nationalarchives.gov.uk/contact-us/
    </body>
  </xsl:template>
</xsl:stylesheet>
