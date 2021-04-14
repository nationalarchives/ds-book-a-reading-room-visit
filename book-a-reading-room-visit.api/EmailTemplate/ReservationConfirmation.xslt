<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <body style="font-family:Arial;font-size:9pt">
      <p>
        Dear <xsl:value-of select="Name" />,
      </p>

      <p>
        You have made a provisional booking to visit the reading rooms at The National Archives on <xsl:value-of select="VisitStartDate" />.
      </p>

      <p>
        To confirm your booking, tell us which documents you want to view during your visit. If you haven’t already done so, follow this link: <xsl:value-of select="ReturnURL" />.
        You will need your booking reference and reader's ticket number to complete this step.
      </p>

      <p>
        If you do not complete this step by <xsl:value-of select="CompleteByDate" /> British Summer Time (BST), your <b>provisional booking will be automatically cancelled.</b>
      </p>

      <p>You can ignore this step if you have already completed your document order but you can return and edit your order as many times as you’d like before the deadline above.</p>

      <h3>Your booking summary </h3>
      <p>
      Your booking reference is: <xsl:value-of select="BookingReference" /><br/>
      Your Reader’s ticket number is: <xsl:value-of select="ReaderTicket" />
      </p>
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
          <xsl:if test="string(ReadingRoom)">
            <tr>
              <th>Reading room</th>
              <td>
                <xsl:value-of select="ReadingRoom" />
              </td>
            </tr>
          </xsl:if>
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

      <p>Use Discovery, our catalogue, to gather your booking references: http://discovery.nationalarchives.gov.uk/ </p>
      <h3>Cancel your visit</h3>
      <p>
        You can cancel your visit at any time. Use this link: <xsl:value-of select="ReturnURL" />
      </p>

      <h3>Need help?</h3>
      <p>This email inbox is not being monitored. Contact us if you need help with this service: https://www.nationalarchives.gov.uk/contact-us/</p>
    </body>
  </xsl:template>
</xsl:stylesheet>
