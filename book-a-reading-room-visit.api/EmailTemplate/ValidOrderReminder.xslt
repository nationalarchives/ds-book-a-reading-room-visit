<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <body style="font-family:Arial;font-size:9pt">
      <p>
        Dear <xsl:value-of select="Name" />,
      </p>
      <p>
        We are looking forward to seeing you soon. See your current document order below - if you would like to edit it, you can still do so before <b>
          <xsl:value-of select="CompleteByDate" /> British Summer Time (BST).
        </b>. After this deadline has passed, we will begin preparing your document order.
      </p>

      <p>
        View or edit my document order: <xsl:value-of select="ReturnURL" />.
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

      <h3>Cancel your visit</h3>
      You can cancel your visit at any time. Use this link: <xsl:value-of select="ReturnURL" />

      <h3>Need help?</h3>

      This email inbox is not being monitored. Contact us if you need help with this service: https://www.nationalarchives.gov.uk/contact-us/
    </body>
  </xsl:template>
</xsl:stylesheet>
